// Program to parse non-standard date strings using TryParseExact
using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.Write("Enter a date string (e.g., 15-Aug-2026 or 2026/08/15): ");
        string input = Console.ReadLine();

        string[] formats = { "dd-MMM-yyyy", "yyyy/MM/dd", "MM-dd-yyyy", "dd/MM/yyyy" };
        DateTime parsedDate;

        if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
        {
            Console.WriteLine($"Parsed Date: {parsedDate:yyyy-MM-dd}");
        }
        else
        {
            Console.WriteLine("Failed to parse date. Please enter in a recognized format.");
        }
    }
}