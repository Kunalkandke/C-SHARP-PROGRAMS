// Program to Merge Two Dictionaries, summing values for overlapping keys
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, int> dict1 = new Dictionary<string, int>
        {
            { "A", 10 },
            { "B", 20 },
            { "C", 30 }
        };

        Dictionary<string, int> dict2 = new Dictionary<string, int>
        {
            { "B", 15 },
            { "C", 25 },
            { "D", 40 }
        };

        Dictionary<string, int> merged = new Dictionary<string, int>(dict1);

        foreach (var kvp in dict2)
        {
            if (merged.ContainsKey(kvp.Key))
                merged[kvp.Key] += kvp.Value;
            else
                merged[kvp.Key] = kvp.Value;
        }

        Console.WriteLine("Merged Dictionary:");
        foreach (var kvp in merged)
        {
            Console.WriteLine($"{kvp.Key} : {kvp.Value}");
        }
    }
}