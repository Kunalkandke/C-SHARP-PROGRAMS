using System;

class Sample
{
    ~Sample()
    {
        Console.WriteLine("Destructor Called");
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
    }
}