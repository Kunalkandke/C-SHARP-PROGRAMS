

// Simulated demo — runs two background workers using Tasks + CancellationToken
using System;
using System.Threading;
using System.Threading.Tasks;

abstract class BackgroundService
{
    public Task StartAsync(CancellationToken ct) => ExecuteAsync(ct);
    protected abstract Task ExecuteAsync(CancellationToken stoppingToken);
}

class EmailQueueWorker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        Console.WriteLine("[EmailQueueWorker] Starting...");
        int cycle = 1;
        while (!ct.IsCancellationRequested)
        {
            Console.WriteLine($"[EmailQueueWorker] Cycle {cycle++}: Checking email queue...");
            await Task.Delay(100, ct);   // simulate processing (100 ms = 1 sec in real)
            Console.WriteLine($"[EmailQueueWorker] Processed pending emails.");
            await Task.Delay(400, ct);   // wait before next check
        }
        Console.WriteLine("[EmailQueueWorker] Stopped.");
    }
}

class DatabaseCleanupWorker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        Console.WriteLine("[DbCleanupWorker ] Starting...");
        int cycle = 1;
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(600, ct);   // first run after delay
            if (ct.IsCancellationRequested) break;
            Console.WriteLine($"[DbCleanupWorker ] Cycle {cycle++}: Running DB cleanup...");
            await Task.Delay(100, ct);
            Console.WriteLine("[DbCleanupWorker ] Old records cleaned up.");
        }
        Console.WriteLine("[DbCleanupWorker ] Stopped.");
    }
}

class BackgroundWorkerHostedService
{
    static async Task Main()
    {
        Console.WriteLine("=== Background Worker using Hosted Services ===\n");

        using var cts = new CancellationTokenSource();

        var emailWorker  = new EmailQueueWorker();
        var cleanupWorker = new DatabaseCleanupWorker();

        // Start both workers concurrently
        var t1 = emailWorker.StartAsync(cts.Token);
        var t2 = cleanupWorker.StartAsync(cts.Token);

        // Let them run for 2 seconds then gracefully stop
        await Task.Delay(2000);

        Console.WriteLine("\n[Host] Sending cancellation signal (graceful shutdown)...\n");
        cts.Cancel();

        await Task.WhenAny(Task.WhenAll(t1, t2), Task.Delay(1000));
        Console.WriteLine("\n[Host] All workers stopped. Application exiting.");
    }
}
