// Program to capitalize the first letter of each word without using built-in TitleCase methods
using System;

class Program
{
    static void Main()
    {
        string input = "hello world from csharp";
        Console.WriteLine("Original string: " + input);
        Console.WriteLine("Capitalized string: " + CapitalizeWords(input));
    }

    static string CapitalizeWords(string str)
    {
        if (string.IsNullOrEmpty(str)) return str;

        char[] chars = str.ToCharArray();
        bool capitalizeNext = true;

        for (int i = 0; i < chars.Length; i++)
        {
            if (char.IsWhiteSpace(chars[i]))
            {
                capitalizeNext = true;
            }
            else if (capitalizeNext)
            {
                chars[i] = char.ToUpper(chars[i]);
                capitalizeNext = false;
            }
        }

        return new string(chars);
    }
}