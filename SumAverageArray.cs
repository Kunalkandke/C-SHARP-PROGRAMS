using System;

class Program
{
    static void Main()
    {
        int[] arr = { 10, 20, 30, 40, 50 };
        int sum = 0;

        for (int i = 0; i < arr.Length; i++)
            sum += arr[i];

        double avg = (double)sum / arr.Length;

        Console.WriteLine("Sum = " + sum);
        Console.WriteLine("Average = " + avg);
    }
}
