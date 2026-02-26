using System;

sealed class FinalClass
{
    public void Display()
    {
        Console.WriteLine("This is a sealed class.");
    }
}

class Program
{
    static void Main()
    {
        FinalClass obj = new FinalClass();
        obj.Display();
    }
}