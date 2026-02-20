using System;

class Program
{
    static void DisplayArray(int[] arr)
    {
        Console.WriteLine("Array elements:");
        foreach (int num in arr)
            Console.WriteLine(num);
    }

    static void Main()
    {
        int[] numbers = { 10, 20, 30, 40 };
        DisplayArray(numbers);
    }
}