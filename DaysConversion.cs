using System;

class Program
{
    static void Main()
    {
        int totalDays, years, months, days;

        Console.Write("Enter number of days: ");
        totalDays = Convert.ToInt32(Console.ReadLine());

        years = totalDays / 365;
        months = (totalDays % 365) / 30;
        days = (totalDays % 365) % 30;

        Console.WriteLine("Years = " + years);
        Console.WriteLine("Months = " + months);
        Console.WriteLine("Days = " + days);
    }
}
