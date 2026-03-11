// Program to demonstrate Task Parallel Library (TPL)
using System;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Task t = Task.Run(() =>
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Task Running: " + i);
            }
        });

        t.Wait();
    }
}