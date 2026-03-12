// Program to demonstrate async and await 
using System;
using System.Threading.Tasks;

class Program
{
    static async Task ShowMessage()
    {
        await Task.Delay(2000);
        Console.WriteLine("Async Method Executed");
    }

    static async Task Main(string[] args)
    {
        await ShowMessage();
        Console.WriteLine("Main Method Finished");
    }
}
