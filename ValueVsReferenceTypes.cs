using System;

class Program
{
    static void Main()
    {
        // Value type
        int a = 10;
        int b = a;
        b = 20;

        Console.WriteLine("a = " + a);  // 10
        Console.WriteLine("b = " + b);  // 20

        // Reference type
        int[] arr1 = { 1, 2, 3 };
        int[] arr2 = arr1;
        arr2[0] = 99;

        Console.WriteLine("arr1[0] = " + arr1[0]); // 99
    }
}