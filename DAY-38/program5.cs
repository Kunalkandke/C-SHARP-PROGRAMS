// Program to calculate Exact Age (Years, Months, Days)
using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Birth Date (yyyy-MM-dd): ");
        DateTime birthDate = DateTime.Parse(Console.ReadLine());

        DateTime today = DateTime.Today;

        int years = today.Year - birthDate.Year;
        int months = today.Month - birthDate.Month;
        int days = today.Day - birthDate.Day;

        if (days < 0)
        {
            months--;
            days += DateTime.DaysInMonth(today.Year, (today.Month == 1 ? 12 : today.Month - 1));
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        Console.WriteLine($"Exact Age: {years} Years, {months} Months, {days} Days");
    }
}