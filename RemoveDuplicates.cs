using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] arr = { 1, 2, 2, 3, 4, 4, 5 };

        var unique = arr.Distinct();

        Console.WriteLine("After removing duplicates:");
        foreach (int i in unique)
            Console.WriteLine(i);
    }
}
