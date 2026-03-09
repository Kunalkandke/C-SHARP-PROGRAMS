using System;

static class StringExtension
{
    public static string ToUpperCustom(this string text)
    {
        return text.ToUpper();
    }
}

class Program
{
    static void Main()
    {
        string name = "amit";
        Console.WriteLine(name.ToUpperCustom());
    }
}