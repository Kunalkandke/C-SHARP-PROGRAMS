using System;

class Program
{
    static void Main()
    {
        int[] arr = { 5, 2, 8, 1, 3 };

        Array.Sort(arr);

        Console.WriteLine("Sorted array:");
        foreach (int i in arr)
            Console.WriteLine(i);
    }
}
