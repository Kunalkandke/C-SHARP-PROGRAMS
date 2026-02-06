using System;

class Demo
{
    public static void Run()
    {
        int n, a = 0, b = 1, c;

        Console.WriteLine("Enter number of terms:");
        n = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Fibonacci Series:");
        Console.Write(a + " " + b + " ");

        for (int i = 3; i <= n; i++)
        {
            c = a + b;
            Console.Write(c + " ");
            a = b;
            b = c;
        }
    }
}
