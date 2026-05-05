// Program to perform a Fire-and-Forget task safely
using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Fire-and-forget task
        Task.Run(() => DoWorkAsync());

        Console.WriteLine("Main thread continues without waiting...");
        Console.ReadLine(); // Prevent program from exiting immediately
    }

    static async Task DoWorkAsync()
    {
        try
        {
            await Task.Delay(2000); // Simulate work
            Console.WriteLine("Fire-and-forget task completed.");
        }
        catch (Exception ex)
        {
            // Handle exceptions to prevent unobserved task exceptions
            Console.WriteLine($"Exception in fire-and-forget task: {ex.Message}");
        }
    }
}