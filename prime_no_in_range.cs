using System;

class Program
{
    static void Main()
    {
        int start, end;

        Console.WriteLine("Enter starting number:");
        start = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter ending number:");
        end = Convert.ToInt32(Console.ReadLine());

        for (int i = start; i <= end; i++)
        {
            int count = 0;

            for (int j = 1; j <= i; j++)
            {
                if (i % j == 0)
                    count++;
            }

            if (count == 2)
                Console.Write(i + " ");
        }
    }
}
