using System;

class Program
{
    static void Main()
    {
        int a, b, temp;

        Console.Write("Enter first number: ");
        a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        b = Convert.ToInt32(Console.ReadLine());

        temp = a;
        a = b;
        b = temp;

        Console.WriteLine("After swapping:");
        Console.WriteLine("a = " + a);
        Console.WriteLine("b = " + b);
    }
}
