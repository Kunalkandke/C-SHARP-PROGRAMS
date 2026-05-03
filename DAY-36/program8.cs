// Program to extract IP addresses from a log file using Regex
using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string filePath = "log.txt"; // Path to log file
        string pattern = @"\b\d{1,3}(\.\d{1,3}){3}\b"; // Regex pattern for IPv4

        Regex regex = new Regex(pattern);

        foreach (var line in File.ReadLines(filePath))
        {
            foreach (Match match in regex.Matches(line))
            {
                Console.WriteLine("Found IP: " + match.Value);
            }
        }
    }
}