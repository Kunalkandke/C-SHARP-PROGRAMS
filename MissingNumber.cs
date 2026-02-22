using System;

class Program
{
    static void Main()
    {
        int[] arr = { 1, 2, 4, 5 };
        int n = 5;

        int totalSum = n * (n + 1) / 2;
        int arrSum = 0;

        foreach (int num in arr)
            arrSum += num;

        Console.WriteLine("Missing Number = " + (totalSum - arrSum));
    }
}