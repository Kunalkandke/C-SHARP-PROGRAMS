using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine();

        string result = Regex.Replace(input, "[^a-zA-Z0-9 ]", "");

        Console.WriteLine("After removing special characters: " + result);
    }
}