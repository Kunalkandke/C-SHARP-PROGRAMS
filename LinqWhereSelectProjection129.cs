using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6 };

        var result = numbers
                     .Where(n => n % 2 == 0)
                     .Select(n => n * 10);

        foreach (var num in result)
        {
            Console.WriteLine(num);
        }
    }
}