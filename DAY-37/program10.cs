// Program to restrict concurrent access using SemaphoreSlim
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static SemaphoreSlim semaphore = new SemaphoreSlim(2); // Allow 2 concurrent accesses

    static async Task Main()
    {
        Task[] tasks = new Task[5];

        for (int i = 0; i < tasks.Length; i++)
        {
            int taskNum = i + 1;
            tasks[i] = Task.Run(async () =>
            {
                await semaphore.WaitAsync();
                try
                {
                    Console.WriteLine($"Task {taskNum} entered critical section.");
                    await Task.Delay(1000); // Simulate work
                    Console.WriteLine($"Task {taskNum} leaving critical section.");
                }
                finally
                {
                    semaphore.Release();
                }
            });
        }

        await Task.WhenAll(tasks);
        Console.WriteLine("All tasks completed.");
    }
}