// Program to Calculate Time Zone Conversion with DST Adjustment
using System;
using System.Collections.Generic;

class TimeZoneConverter
{
    static void Main()
    {
        Dictionary<string, string> timeZoneMap = new Dictionary<string, string>
        {
            { "1", "UTC" },
            { "2", "Eastern Standard Time" },
            { "3", "Central Standard Time" },
            { "4", "Mountain Standard Time" },
            { "5", "Pacific Standard Time" },
            { "6", "India Standard Time" },
            { "7", "Tokyo Standard Time" },
            { "8", "GMT Standard Time" },
            { "9", "AUS Eastern Standard Time" },
            { "10", "China Standard Time" }
        };

        Console.WriteLine("=== Time Zone Converter with DST Adjustment ===\n");

        Console.WriteLine("Available Time Zones:");
        foreach (var tz in timeZoneMap)
            Console.WriteLine($"  {tz.Key}. {tz.Value}");

        Console.Write("\nEnter date and time (yyyy-MM-dd HH:mm:ss): ");
        string input = Console.ReadLine();

        if (!DateTime.TryParse(input, out DateTime inputDateTime))
        {
            Console.WriteLine("Invalid date/time format.");
            return;
        }

        Console.Write("Enter SOURCE time zone number: ");
        string sourceKey = Console.ReadLine();

        Console.Write("Enter TARGET time zone number: ");
        string targetKey = Console.ReadLine();

        if (!timeZoneMap.ContainsKey(sourceKey) || !timeZoneMap.ContainsKey(targetKey))
        {
            Console.WriteLine("Invalid time zone selection.");
            return;
        }

        TimeZoneInfo sourceZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneMap[sourceKey]);
        TimeZoneInfo targetZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneMap[targetKey]);

        DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(inputDateTime, sourceZone);
        DateTime targetTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, targetZone);

        Console.WriteLine("\n========== Conversion Result ==========");
        Console.WriteLine($"  Input DateTime     : {inputDateTime:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($"  Source Time Zone   : {sourceZone.DisplayName}");
        Console.WriteLine($"  Target Time Zone   : {targetZone.DisplayName}");
        Console.WriteLine($"  UTC Time           : {utcTime:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($"  Converted DateTime : {targetTime:yyyy-MM-dd HH:mm:ss}");

        bool sourceDST = sourceZone.IsDaylightSavingTime(inputDateTime);
        bool targetDST = targetZone.IsDaylightSavingTime(targetTime);

        Console.WriteLine($"  Source DST Active  : {(sourceDST ? "Yes" : "No")}");
        Console.WriteLine($"  Target DST Active  : {(targetDST ? "Yes" : "No")}");

        TimeSpan sourceOffset = sourceZone.GetUtcOffset(inputDateTime);
        TimeSpan targetOffset = targetZone.GetUtcOffset(targetTime);

        Console.WriteLine($"  Source UTC Offset  : {(sourceOffset >= TimeSpan.Zero ? "+" : "")}{sourceOffset:hh\\:mm}");
        Console.WriteLine($"  Target UTC Offset  : {(targetOffset >= TimeSpan.Zero ? "+" : "")}{targetOffset:hh\\:mm}");

        TimeSpan difference = targetOffset - sourceOffset;
        Console.WriteLine($"  Time Difference    : {(difference >= TimeSpan.Zero ? "+" : "")}{difference:hh\\:mm} hours");
        Console.WriteLine("=======================================");
    }
}