// Program to find the first Non-Repeating Character in a string
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string input = "swiss";
        Dictionary<char, int> countMap = new Dictionary<char, int>();

        foreach (char ch in input)
        {
            if (countMap.ContainsKey(ch))
                countMap[ch]++;
            else
                countMap[ch] = 1;
        }

        char firstNonRepeating = '\0';
        foreach (char ch in input)
        {
            if (countMap[ch] == 1)
            {
                firstNonRepeating = ch;
                break;
            }
        }

        if (firstNonRepeating != '\0')
            Console.WriteLine("First non-repeating character: " + firstNonRepeating);
        else
            Console.WriteLine("No non-repeating character found.");
    }
}