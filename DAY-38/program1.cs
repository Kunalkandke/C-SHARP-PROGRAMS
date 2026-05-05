// Program to use Interlocked operations for thread-safe increments
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static int counter = 0;

    static void Main()
    {
        Task[] tasks = new Task[5];

        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    Interlocked.Increment(ref counter);
                }
            });
        }

        Task.WaitAll(tasks);

        Console.WriteLine($"Final counter value: {counter}");
    }
}
