// Program to catch and handle AggregateException in concurrent tasks
using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Task task1 = Task.Run(() => { throw new InvalidOperationException("Task 1 failed"); });
        Task task2 = Task.Run(() => { throw new ArgumentException("Task 2 failed"); });

        try
        {
            Task.WaitAll(task1, task2);
        }
        catch (AggregateException ae)
        {
            Console.WriteLine("Caught AggregateException:");
            foreach (var ex in ae.InnerExceptions)
            {
                Console.WriteLine($" - {ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}