using System;

class Program
{
    static void Main()
    {
        int a, b, sum;

        Console.Write("Enter first number: ");
        a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        b = Convert.ToInt32(Console.ReadLine());

        sum = a + b;
        Console.WriteLine("Sum = " + sum);
    }
}
