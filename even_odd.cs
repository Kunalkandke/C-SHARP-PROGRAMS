using System;

class Program
{
    static void Main()
    {
        int num;

        Console.Write("Enter a number: ");
        num = Convert.ToInt32(Console.ReadLine());

        if (num % 2 == 0)
            Console.WriteLine("Even number");
        else
            Console.WriteLine("Odd number");
    }
}
