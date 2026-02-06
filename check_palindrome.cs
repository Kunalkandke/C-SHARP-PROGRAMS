using System;

class Program
{
    static void Main()
    {
        int n, temp, rev = 0;

        Console.WriteLine("Enter a number:");
        n = Convert.ToInt32(Console.ReadLine());

        temp = n;

        while (n > 0)
        {
            rev = rev * 10 + (n % 10);
            n = n / 10;
        }

        if (temp == rev)
            Console.WriteLine("Palindrome Number");
        else
            Console.WriteLine("Not a Palindrome Number");
    }
}
