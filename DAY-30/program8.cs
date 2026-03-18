// 218. Program to flatten a Jagged Array into a single-dimensional array
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[][] jaggedArray = new int[][]
        {
            new int[] {1, 2, 3},
            new int[] {4, 5},
            new int[] {6, 7, 8, 9}
        };

        int[] flattenedArray = jaggedArray.SelectMany(inner => inner).ToArray();

        Console.WriteLine("Flattened Array:");
        foreach (int item in flattenedArray)
        {
            Console.Write(item + " ");
        }
    }
}