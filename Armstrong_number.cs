using System;

class Program
{
    static void Main()
    {
        int n, temp, sum = 0, digit;

        Console.WriteLine("Enter a number:");
        n = Convert.ToInt32(Console.ReadLine());

        temp = n;

        while (n > 0)
        {
            digit = n % 10;
            sum = sum + (digit * digit * digit);
            n = n / 10;
        }

        if (temp == sum)
            Console.WriteLine("Armstrong Number");
        else
            Console.WriteLine("Not an Armstrong Number");
    }
}
