using System;

class Program
{
    static void Main()
    {
        int[,] a = { { 1, 2, 3 }, { 4, 5, 6 } };

        Console.WriteLine("Transpose:");

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Console.Write(a[j, i] + " ");
            }
            Console.WriteLine();
        }
    }
}
