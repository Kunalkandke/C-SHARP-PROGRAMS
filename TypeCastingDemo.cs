using System;

class Program
{
    static void Main()
    {
        double num = 10.75;
        int result;

        result = (int)num;   // explicit casting

        Console.WriteLine("Original value = " + num);
        Console.WriteLine("After casting to int = " + result);
    }
}
