using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> items = new List<string>();

        items.Add("Laptop");
        items.Add("Mouse");
        items.Add("Keyboard");

        Console.WriteLine("Inventory Items:");

        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}