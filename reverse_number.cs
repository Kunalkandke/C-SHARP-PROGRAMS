using System;

class Program
{
    static void Main()
    {
        int n, rev = 0;

        Console.WriteLine("Enter a number:");
        n = Convert.ToInt32(Console.ReadLine());

        while (n > 0)
        {
            rev = rev * 10 + (n % 10);
            n = n / 10;
        }

        Console.WriteLine("Reversed number = " + rev);
    }
}
