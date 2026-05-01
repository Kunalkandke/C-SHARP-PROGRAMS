// Program to safely Remove Elements from a List while iterating
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

        // Remove even numbers safely
        for (int i = numbers.Count - 1; i >= 0; i--)
        {
            if (numbers[i] % 2 == 0)
            {
                numbers.RemoveAt(i);
            }
        }

        Console.WriteLine("List after removing even numbers:");
        foreach (var num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}