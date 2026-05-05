// Program to check if a specific DateTime falls on a holiday
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Define a list of holidays
        List<DateTime> holidays = new List<DateTime>
        {
            new DateTime(2026, 1, 1),   // New Year's Day
            new DateTime(2026, 12, 25), // Christmas
            new DateTime(2026, 8, 15)   // Example holiday
        };

        Console.Write("Enter a date (yyyy-MM-dd): ");
        DateTime inputDate = DateTime.Parse(Console.ReadLine());

        bool isHoliday = holidays.Exists(d => d.Date == inputDate.Date);

        if (isHoliday)
            Console.WriteLine($"{inputDate:yyyy-MM-dd} is a holiday.");
        else
            Console.WriteLine($"{inputDate:yyyy-MM-dd} is not a holiday.");
    }
}