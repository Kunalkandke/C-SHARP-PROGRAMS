using System;

class Sample
{
    public Sample()
    {
        Console.WriteLine("Object Created");
    }

    ~Sample()
    {
        Console.WriteLine("Object Destroyed");
    }
}

class Program
{
    static void Main()
    {
        Sample obj = new Sample();
        obj = null;

        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Garbage Collection Done");
    }
}