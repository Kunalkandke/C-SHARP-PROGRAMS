// Program to implement a Thread-Safe Singleton Design Pattern
using System;

class Singleton
{
    private static Singleton instance = null;
    private static readonly object lockObj = new object();

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }

    public void DisplayMessage()
    {
        Console.WriteLine("Singleton instance accessed.");
    }
}

class Program
{
    static void Main()
    {
        Singleton s1 = Singleton.Instance;
        Singleton s2 = Singleton.Instance;

        s1.DisplayMessage();
        Console.WriteLine("Are both instances same? " + (s1 == s2));
    }
}