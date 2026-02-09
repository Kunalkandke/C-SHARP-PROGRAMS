using System;

class Program
{
    static void Main()
    {
        int n, sum = 0;

        Console.Write("Enter n: ");
        n = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            sum += i;
        }

        Console.WriteLine("Sum = " + sum);
    }
}
