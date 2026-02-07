using System;

class Program
{
    static void Main()
    {
        char ch;

        Console.Write("Enter a character: ");
        ch = Convert.ToChar(Console.ReadLine());

        int ascii = (int)ch;

        Console.WriteLine("ASCII value = " + ascii);
    }
}
