// Program to Reverse the Order of Words in a sentence
using System;

class Program
{
    static void Main()
    {
        string sentence = "Hello world from CSharp";
        Console.WriteLine("Original sentence: " + sentence);
        Console.WriteLine("Reversed sentence: " + ReverseWords(sentence));
    }

    static string ReverseWords(string str)
    {
        string[] words = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Array.Reverse(words);
        return string.Join(" ", words);
    }
}