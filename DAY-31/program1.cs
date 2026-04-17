// 221. Program to find the Longest Substring without repeating characters
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string input = "abcabcbb";
        int maxLength = 0;
        int start = 0;
        Dictionary<char, int> seen = new Dictionary<char, int>();

        for (int end = 0; end < input.Length; end++)
        {
            char current = input[end];
            if (seen.ContainsKey(current) && seen[current] >= start)
            {
                start = seen[current] + 1;
            }
            seen[current] = end;
            maxLength = Math.Max(maxLength, end - start + 1);
        }

        Console.WriteLine("Length of longest substring without repeating characters: " + maxLength);
    }
}
