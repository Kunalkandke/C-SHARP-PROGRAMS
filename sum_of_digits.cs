using System;

class Program
{
    static void Main()
    {
        int n, sum = 0;

        Console.WriteLine("Enter a number:");
        n = Convert.ToInt32(Console.ReadLine());

        while (n > 0)
        {
            sum = sum + (n % 10);
            n = n / 10;
        }

        Console.WriteLine("Sum of digits = " + sum);
    }
}
