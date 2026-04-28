// Program to count exact occurrences of a specific word in a text block
using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string text = "CSharp is great. Learning CSharp improves your CSharp skills.";
        string wordToFind = "CSharp";

        int count = CountWordOccurrences(text, wordToFind);
        Console.WriteLine($"The word \"{wordToFind}\" occurs {count} times.");
    }

    static int CountWordOccurrences(string text, string word)
    {
        string pattern = $@"\b{Regex.Escape(word)}\b";
        return Regex.Matches(text, pattern, RegexOptions.IgnoreCase).Count;
    }
}