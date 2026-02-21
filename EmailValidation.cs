using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.Write("Enter email: ");
        string email = Console.ReadLine();

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        if (Regex.IsMatch(email, pattern))
            Console.WriteLine("Valid Email");
        else
            Console.WriteLine("Invalid Email");
    }
}