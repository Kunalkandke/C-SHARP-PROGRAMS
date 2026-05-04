// Program to implement a Producer-Consumer queue using BlockingCollection
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        BlockingCollection<int> queue = new BlockingCollection<int>(boundedCapacity: 5);

        // Producer Task
        Task producer = Task.Run(() =>
        {
            for (int i = 1; i <= 10; i++)
            {
                queue.Add(i);
                Console.WriteLine($"Produced: {i}");
                Thread.Sleep(200);
            }
            queue.CompleteAdding();
        });

        // Consumer Task
        Task consumer = Task.Run(() =>
        {
            foreach (var item in queue.GetConsumingEnumerable())
            {
                Console.WriteLine($"Consumed: {item}");
                Thread.Sleep(500);
            }
        });

        Task.WaitAll(producer, consumer);
        Console.WriteLine("Producer-Consumer processing completed.");
    }
}