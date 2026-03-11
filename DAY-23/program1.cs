// Program to demonstrate multithreading
using System;
using System.Threading;

class Program
{
    static void PrintNumbers()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine(i);
        }
    }

    static void Main(string[] args)
    {
        Thread t = new Thread(PrintNumbers);
        t.Start();
    }
}
