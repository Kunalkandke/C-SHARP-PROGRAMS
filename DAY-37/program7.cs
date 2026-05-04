// Program to parallelize a loop using Parallel.For
using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Starting Parallel.For loop...");

        Parallel.For(0, 10, i =>
        {
            Console.WriteLine($"Processing index {i} on thread {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            Task.Delay(500).Wait(); // Simulate work
        });

        Console.WriteLine("Parallel.For loop completed.");
    }
}