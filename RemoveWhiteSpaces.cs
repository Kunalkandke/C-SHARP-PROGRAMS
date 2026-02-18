using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string str = Console.ReadLine();

        string result = str.Replace(" ", "");

        Console.WriteLine("After removing spaces: " + result);
    }
}
