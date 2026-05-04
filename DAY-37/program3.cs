// Program to Cancel a Task using CancellationToken
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        Task longRunningTask = Task.Run(() => DoWork(cts.Token), cts.Token);

        Console.WriteLine("Press Enter to cancel the task...");
        Console.ReadLine();
        cts.Cancel();

        try
        {
            await longRunningTask;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Task was canceled.");
        }
    }

    static void DoWork(CancellationToken token)
    {
        for (int i = 1; i <= 10; i++)
        {
            token.ThrowIfCancellationRequested();
            Console.WriteLine($"Working... {i}");
            Thread.Sleep(500);
        }
        Console.WriteLine("Task completed successfully.");
    }
}