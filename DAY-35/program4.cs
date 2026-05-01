// Program to filter a Dictionary<K,V> by specific value criteria
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<string, int> scores = new Dictionary<string, int>
        {
            { "Alice", 85 },
            { "Bob", 92 },
            { "Charlie", 78 },
            { "David", 90 }
        };

        // Filter dictionary to include only scores >= 90
        var filtered = scores.Where(kvp => kvp.Value >= 90)
                             .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        Console.WriteLine("Filtered Dictionary (scores >= 90):");
        foreach (var kvp in filtered)
        {
            Console.WriteLine($"{kvp.Key} : {kvp.Value}");
        }
    }
}