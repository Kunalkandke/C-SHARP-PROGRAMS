using System;

class Program
{
    static void Main()
    {
        int[] arr1 = { 1, 2, 3, 4, 5 };
        int[] arr2 = { 3, 4, 5, 6, 7 };

        Console.WriteLine("Common elements:");

        foreach (int num1 in arr1)
        {
            foreach (int num2 in arr2)
            {
                if (num1 == num2)
                    Console.WriteLine(num1);
            }
        }
    }
}