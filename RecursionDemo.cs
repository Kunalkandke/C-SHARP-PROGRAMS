using System;

class Program
{
    static void PrintNumbers(int n)
    {
        if (n == 0)
            return;

        Console.WriteLine(n);
        PrintNumbers(n - 1);
    }

    static void Main()
    {
        PrintNumbers(5);
    }
}
