// Program to demonstrate and resolve a Deadlock scenario
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static object lock1 = new object();
    static object lock2 = new object();

    static void Main()
    {
        Console.WriteLine("Demonstrating deadlock scenario...");

        Task t1 = Task.Run(() => DeadlockMethod(lock1, lock2));
        Task t2 = Task.Run(() => DeadlockMethod(lock2, lock1));

        Task.WaitAll(t1, t2); // This will hang due to deadlock
    }

    static void DeadlockMethod(object firstLock, object secondLock)
    {
        lock (firstLock)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} acquired first lock");
            Thread.Sleep(500); // Simulate work

            lock (secondLock)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} acquired second lock");
            }
        }
    }
}