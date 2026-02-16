using System;

class Program
{
    static void Main()
    {
        string str;
        int count = 0;

        Console.Write("Enter a string: ");
        str = Console.ReadLine().ToLower();

        foreach (char ch in str)
        {
            if ("aeiou".Contains(ch))
                count++;
        }

        Console.WriteLine("Number of vowels = " + count);
    }
}
