// Program to find the Top N Maximum Numbers in a list using LINQ
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 5, 12, 7, 20, 3, 15, 8 };
        int N = 3;

        var topN = numbers.OrderByDescending(n => n).Take(N);

        Console.WriteLine($"Top {N} maximum numbers:");
        foreach (var num in topN)
        {
            Console.WriteLine(num);
        }
    }
}
