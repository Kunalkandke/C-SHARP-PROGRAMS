// Program to implement an Interface with Default Implementations (C# 8.0+)
using System;

interface IPrinter
{
    void Print(string message);

    // Default implementation
    void PrintWithTimestamp(string message)
    {
        Console.WriteLine($"[{DateTime.Now}] {message}");
    }
}

class ConsolePrinter : IPrinter
{
    public void Print(string message)
    {
        Console.WriteLine(message);
    }
}

class Program
{
    static void Main()
    {
        IPrinter printer = new ConsolePrinter();

        printer.Print("Hello, World!");
        printer.PrintWithTimestamp("Hello with timestamp!");
    }
}