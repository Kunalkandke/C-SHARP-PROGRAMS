// Program to calculate Working Days between two dates (excluding weekends)
using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Start Date (yyyy-MM-dd): ");
        DateTime startDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter End Date (yyyy-MM-dd): ");
        DateTime endDate = DateTime.Parse(Console.ReadLine());

        int workingDays = 0;
        DateTime current = startDate;

        while (current <= endDate)
        {
            if (current.DayOfWeek != DayOfWeek.Saturday && current.DayOfWeek != DayOfWeek.Sunday)
            {
                workingDays++;
            }
            current = current.AddDays(1);
        }

        Console.WriteLine($"Working Days (excluding weekends) between {startDate:yyyy-MM-dd} and {endDate:yyyy-MM-dd}: {workingDays}");
    }
}