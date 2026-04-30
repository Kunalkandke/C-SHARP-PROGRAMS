// Program to implement Pagination Logic (Skip and Take) with LINQ
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> numbers = Enumerable.Range(1, 20).ToList();
        int pageNumber = 2;
        int pageSize = 5;

        var pagedData = numbers
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);

        Console.WriteLine($"Page {pageNumber} (Page Size: {pageSize}):");
        foreach (var num in pagedData)
        {
            Console.WriteLine(num);
        }
    }
}