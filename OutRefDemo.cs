using System;

class Program
{
    static void Calculate(ref int a, out int b)
    {
        a = a + 10;     // ref must be initialized before passing
        b = 20;         // out must be assigned inside method
    }

    static void Main()
    {
        int x = 5;
        int y;

        Calculate(ref x, out y);

        Console.WriteLine("Value of x (ref) = " + x);
        Console.WriteLine("Value of y (out) = " + y);
    }
}
