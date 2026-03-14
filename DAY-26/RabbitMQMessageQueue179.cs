// Message Queue using RabbitMQ (Basic)


// Simulated demo — in-process producer/consumer with channel (no RabbitMQ server)
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

record Order(int OrderId, string Product, int Quantity, DateTime Timestamp);

class SimpleMessageQueue
{
    private readonly ConcurrentQueue<string> _queue = new();
    private readonly SemaphoreSlim _signal = new(0);

    public void Publish(string message)
    {
        _queue.Enqueue(message);
        _signal.Release();
    }

    public async Task<string?> ConsumeAsync(CancellationToken ct)
    {
        await _signal.WaitAsync(ct);
        _queue.TryDequeue(out var msg);
        return msg;
    }
}

class RabbitMQMessageQueue
{
    static async Task Main()
    {
        Console.WriteLine("=== Message Queue using RabbitMQ (simulated) ===\n");

        var queue = new SimpleMessageQueue();
        using var cts = new CancellationTokenSource();

        // Consumer task — runs in background
        var consumerTask = Task.Run(async () =>
        {
            Console.WriteLine("[Consumer] Waiting for messages...\n");
            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    string? msg = await queue.ConsumeAsync(cts.Token);
                    if (msg is null) continue;

                    var order = JsonSerializer.Deserialize<Order>(msg);
                    Console.WriteLine($"[Consumer] Received  => OrderId: {order!.OrderId}, Product: {order.Product}, Qty: {order.Quantity}");
                    await Task.Delay(200, cts.Token);  // simulate processing
                    Console.WriteLine($"[Consumer] Processed => OrderId: {order.OrderId} ✓\n");
                }
                catch (OperationCanceledException) { break; }
            }
            Console.WriteLine("[Consumer] Stopped.");
        }, cts.Token);

        // Producer — publishes 5 orders
        await Task.Delay(100);
        for (int i = 1; i <= 5; i++)
        {
            var order = new Order(i, $"Product {i}", i * 2, DateTime.UtcNow);
            string json = JsonSerializer.Serialize(order);
            queue.Publish(json);
            Console.WriteLine($"[Producer] Published => OrderId: {order.OrderId}, Product: {order.Product}");
            await Task.Delay(150);
        }
        Console.WriteLine("[Producer] All messages published.\n");

        // Wait for consumer to finish processing
        await Task.Delay(2000);
        cts.Cancel();
        await Task.WhenAny(consumerTask, Task.Delay(1000));
        Console.WriteLine("\n[Host] Shutdown complete.");
    }
}
