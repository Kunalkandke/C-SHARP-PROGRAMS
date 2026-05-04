// Program to use ThreadLocal<T> for thread-specific storage
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static ThreadLocal<int> threadLocalData = new ThreadLocal<int>(() =>
    {
        return Thread.CurrentThread.ManagedThreadId * 10;
    });

    static void Main()
    {
        Parallel.For(1, 6, i =>
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} has thread-local value: {threadLocalData.Value}");
        });

        threadLocalData.Dispose();
    }
}
