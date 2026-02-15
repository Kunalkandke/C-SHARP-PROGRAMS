using System;

class Program
{
    static void Main()
    {
        int[] arr1 = { 1, 2, 3 };
        int[] arr2 = { 4, 5, 6 };

        int[] merged = new int[arr1.Length + arr2.Length];

        arr1.CopyTo(merged, 0);
        arr2.CopyTo(merged, arr1.Length);

        Console.WriteLine("Merged array:");
        foreach (int i in merged)
            Console.WriteLine(i);
    }
}
