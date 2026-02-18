using System;

class Program
{
    static int Factorial(int n)
    {
        if (n == 0)
            return 1;

        return n * Factorial(n - 1);
    }

    static void Main()
    {
        Console.Write("Enter number: ");
        int num = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Factorial = " + Factorial(num));
    }
}
