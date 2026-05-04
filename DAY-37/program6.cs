// Program to execute a background method periodically using a Timer
using System;
using System.Threading;

class Program
{
    static Timer timer;

    static void Main()
    {
        timer = new Timer(Callback, null, 0, 2000); // Execute every 2 seconds

        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }

    static void Callback(object state)
    {
        Console.WriteLine($"Background method executed at {DateTime.Now}");
    }
}