using System;

class Program
{
    static int Add(params int[] numbers)
    {
        int sum = 0;
        foreach (int num in numbers)
            sum += num;

        return sum;
    }

    static void Main()
    {
        Console.WriteLine("Sum = " + Add(10, 20, 30));
    }
}
