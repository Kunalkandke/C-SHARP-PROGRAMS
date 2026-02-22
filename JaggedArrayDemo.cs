using System;

class Program
{
    static void Main()
    {
        int[][] jagged = new int[2][];

        jagged[0] = new int[] { 1, 2, 3 };
        jagged[1] = new int[] { 4, 5 };

        Console.WriteLine("Jagged Array Elements:");

        for (int i = 0; i < jagged.Length; i++)
        {
            for (int j = 0; j < jagged[i].Length; j++)
            {
                Console.Write(jagged[i][j] + " ");
            }
            Console.WriteLine();
        }
    }
}