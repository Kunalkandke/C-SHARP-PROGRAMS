using System;

class Program
{
    static void Main()
    {
        int n;

        Console.WriteLine("Enter a number:");
        n = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine(n + " x " + i + " = " + (n * i));
        }
    }
}
