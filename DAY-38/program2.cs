// Program to implement a thread-safe Singleton using Lazy<T>
using System;

class Singleton
{
    private static readonly Lazy<Singleton> instance = new Lazy<Singleton>(() => new Singleton());

    public static Singleton Instance => instance.Value;

    private Singleton()
    {
        Console.WriteLine("Singleton instance created.");
    }

    public void DoSomething()
    {
        Console.WriteLine("Singleton method executed.");
    }
}

class Program
{
    static void Main()
    {
        Singleton s1 = Singleton.Instance;
        s1.DoSomething();

        Singleton s2 = Singleton.Instance;
        s2.DoSomething();

        Console.WriteLine($"Are both instances same? {object.ReferenceEquals(s1, s2)}");
    }
}