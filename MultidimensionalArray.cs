using System;

class Program
{
    static void Main()
    {
        int[,] matrix = { { 1, 2 }, { 3, 4 } };

        Console.WriteLine("Multidimensional Array:");

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}