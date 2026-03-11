// Program to demonstrate performance optimization basics
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();

        long sum = 0;
        for (int i = 1; i <= 1000000; i++)
        {
            sum += i;
        }

        sw.Stop();

        Console.WriteLine("Sum: " + sum);
        Console.WriteLine("Time Taken: " + sw.ElapsedMilliseconds + " ms");
    }
}