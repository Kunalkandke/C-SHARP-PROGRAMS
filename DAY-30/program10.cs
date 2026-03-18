// 220. Program to implement a custom Iterator for a collection using yield return
using System;
using System.Collections.Generic;

class CustomCollection
{
    private List<int> items = new List<int>();

    public void Add(int item)
    {
        items.Add(item);
    }

    public IEnumerable<int> GetItems()
    {
        foreach (var item in items)
        {
            yield return item;
        }
    }
}

class Program
{
    static void Main()
    {
        CustomCollection collection = new CustomCollection();
        collection.Add(10);
        collection.Add(20);
        collection.Add(30);

        Console.WriteLine("Items in collection:");
        foreach (var item in collection.GetItems())
        {
            Console.WriteLine(item);
        }
    }
}
