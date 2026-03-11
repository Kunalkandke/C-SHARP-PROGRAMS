// Program to demonstrate locking and synchronization
using System;
using System.Threading;

class Program
{
    static object obj = new object();

    static void PrintNumbers()
    {
        lock (obj)
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine(i);
            }
        }
    }

    static void Main(string[] args)
    {
        Thread t1 = new Thread(PrintNumbers);
        Thread t2 = new Thread(PrintNumbers);

        t1.Start();
        t2.Start();
    }
}