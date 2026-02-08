using System;

class Program
{
    static void Main()
    {
        for (int i = 1; i <= 10; i++)
        {
            if (i == 5)
                continue;   // skip 5

            if (i == 8)
                break;      // stop loop at 8

            Console.WriteLine(i);
        }
    }
}
