using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = { 2, 3, 4, 5 };

        int product = numbers.Aggregate((a, b) => a * b);

        Console.WriteLine("Product: " + product);
    }
}