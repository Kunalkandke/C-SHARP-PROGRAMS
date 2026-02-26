using System;

static class Utility
{
    public static void ShowMessage()
    {
        Console.WriteLine("This is a static class method.");
    }
}

class Program
{
    static void Main()
    {
        Utility.ShowMessage();
    }
}