// Program to implement a thread-safe dictionary using ConcurrentDictionary
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        ConcurrentDictionary<int, string> concurrentDict = new ConcurrentDictionary<int, string>();

        // Adding items safely from multiple tasks
        Parallel.For(0, 10, i =>
        {
            concurrentDict[i] = "Value " + i;
        });

        // Updating an item safely
        concurrentDict.AddOrUpdate(5, "New Value 5", (key, oldValue) => "Updated " + oldValue);

        // Reading items safely
        foreach (var kvp in concurrentDict)
        {
            Console.WriteLine($"{kvp.Key} : {kvp.Value}");
        }
    }
}