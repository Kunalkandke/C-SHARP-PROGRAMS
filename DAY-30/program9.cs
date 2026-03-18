// Program to find the Union and Intersection of two integer arrays
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] array1 = {1, 2, 3, 4, 5};
        int[] array2 = {4, 5, 6, 7, 8};

        var union = array1.Union(array2).ToArray();
        var intersection = array1.Intersect(array2).ToArray();

        Console.WriteLine("Union of arrays:");
        foreach (var item in union)
            Console.Write(item + " ");

        Console.WriteLine("\nIntersection of arrays:");
        foreach (var item in intersection)
            Console.Write(item + " ");
    }
}