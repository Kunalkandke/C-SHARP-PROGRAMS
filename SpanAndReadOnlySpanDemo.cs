using System;

class Program
{
    static void Main()
    {
        int[] numbers = { 10, 20, 30, 40, 50 };

        Span<int> span = numbers.AsSpan(1, 3);
        span[0] = 99;  // modifies original array

        Console.WriteLine("Modified Array:");
        foreach (var num in numbers)
            Console.Write(num + " ");

        ReadOnlySpan<int> readOnlySpan = numbers;
        Console.WriteLine("\nReadOnlySpan first element: " + readOnlySpan[0]);
    }
}