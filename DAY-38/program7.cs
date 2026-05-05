// Program to find the First and Last Day of any given month/year
using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Year (yyyy): ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Enter Month (1-12): ");
        int month = int.Parse(Console.ReadLine());

        DateTime firstDay = new DateTime(year, month, 1);
        DateTime lastDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));

        Console.WriteLine($"First Day of {month}/{year}: {firstDay:yyyy-MM-dd}");
        Console.WriteLine($"Last Day of {month}/{year}: {lastDay:yyyy-MM-dd}");
    }
}