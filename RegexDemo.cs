using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string input = "My number is 12345";

        Match match = Regex.Match(input, @"\d+");

        if (match.Success)
            Console.WriteLine("Found number: " + match.Value);
        else
            Console.WriteLine("No number found");
    }
}