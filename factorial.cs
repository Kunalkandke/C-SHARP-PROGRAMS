using System;

class Demo
{
    public static void Run()
    {
        int n, fact = 1;

        Console.WriteLine("Enter a number:");
        n = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            fact = fact * i;
        }

        Console.WriteLine("Factorial = " + fact);
    }
}
