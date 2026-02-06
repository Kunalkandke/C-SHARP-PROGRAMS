using System;

class Program
{
    static void Main()
    {
        int n, count = 0;

        Console.WriteLine("Enter a number:");
        n = Convert.ToInt32(Console.ReadLine());

        while (n > 0)
        {
            count++;
            n = n / 10;
        }

        Console.WriteLine("Number of digits = " + count);
    }
}
