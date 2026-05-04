// Program to execute tasks and wait for all using Task.WhenAll
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Task task1 = Task.Run(() => DoWork("Task 1", 2000));
        Task task2 = Task.Run(() => DoWork("Task 2", 1500));
        Task task3 = Task.Run(() => DoWork("Task 3", 1000));

        await Task.WhenAll(task1, task2, task3);

        Console.WriteLine("All tasks completed.");
    }

    static void DoWork(string taskName, int delay)
    {
        Console.WriteLine($"{taskName} started.");
        Task.Delay(delay).Wait();
        Console.WriteLine($"{taskName} completed.");
    }
}
