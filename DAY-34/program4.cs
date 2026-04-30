// Program to perform a Cross Join (Cartesian product) using LINQ
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<string> list1 = new List<string> { "A", "B", "C" };
        List<int> list2 = new List<int> { 1, 2, 3 };

        var crossJoin = from item1 in list1
                        from item2 in list2
                        select new { Item1 = item1, Item2 = item2 };

        Console.WriteLine("Cross Join Result:");
        foreach (var pair in crossJoin)
        {
            Console.WriteLine($"{pair.Item1} - {pair.Item2}");
        }
    }
}