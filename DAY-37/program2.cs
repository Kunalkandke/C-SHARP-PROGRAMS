// Program to return the result of the first task to finish using Task.WhenAny
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Task<string> task1 = Task.Run(async () =>
        {
            await Task.Delay(3000);
            return "Result from Task 1";
        });

        Task<string> task2 = Task.Run(async () =>
        {
            await Task.Delay(1000);
            return "Result from Task 2";
        });

        Task<string> task3 = Task.Run(async () =>
        {
            await Task.Delay(2000);
            return "Result from Task 3";
        });

        Task<string> firstCompleted = await Task.WhenAny(task1, task2, task3);

        Console.WriteLine("First completed task result: " + firstCompleted.Result);
    }
}