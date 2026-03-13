// Logging System using Serilog

// Simulated demo (no NuGet required) — shows the same flow using Console
using System;

class SerilogLoggingSystemDemo
{
    enum LogLevel { Debug, Information, Warning, Error }

    static void Log(LogLevel level, string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss} {level.ToString().ToUpper()}] {message}");
    }

    static void Main()
    {
        Log(LogLevel.Information, "Application Started");

        try
        {
            Log(LogLevel.Debug, "Performing a debug operation");
            int result = Divide(10, 0);
            Log(LogLevel.Information, $"Result: {result}");
        }
        catch (Exception ex)
        {
            Log(LogLevel.Error, $"An error occurred: {ex.Message}");
        }
        finally
        {
            Log(LogLevel.Information, "Application Ending");
        }
    }

    static int Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Cannot divide by zero.");
        return a / b;
    }
}
