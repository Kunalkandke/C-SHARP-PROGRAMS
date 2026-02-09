using System;

class Program
{
    static void Main()
    {
        Random rnd = new Random();

        Console.WriteLine("Random Numbers:");
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine(rnd.Next(1, 101));   // 1 to 100
        }
    }
}
