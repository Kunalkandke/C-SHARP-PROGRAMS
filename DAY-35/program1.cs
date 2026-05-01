// Program to find the Symmetric Difference between two HashSet<T> collections
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        HashSet<int> set1 = new HashSet<int> { 1, 2, 3, 4 };
        HashSet<int> set2 = new HashSet<int> { 3, 4, 5, 6 };

        HashSet<int> symmetricDiff = new HashSet<int>(set1);
        symmetricDiff.SymmetricExceptWith(set2);

        Console.WriteLine("Symmetric Difference:");
        foreach (var item in symmetricDiff)
        {
            Console.WriteLine(item);
        }
    }
}
