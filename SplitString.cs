using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a sentence: ");
        string input = Console.ReadLine();

        string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine("Words:");
        foreach (string word in words)
            Console.WriteLine(word);
    }
}