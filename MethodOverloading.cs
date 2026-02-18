using System;

class Program
{
    static int Add(int a, int b)
    {
        return a + b;
    }

    static double Add(double a, double b)
    {
        return a + b;
    }

    static void Main()
    {
        Console.WriteLine(Add(5, 10));
        Console.WriteLine(Add(5.5, 2.5));
    }
}
