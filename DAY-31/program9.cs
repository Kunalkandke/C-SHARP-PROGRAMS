// Program to convert a Roman numeral string into an integer
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string roman = "MCMLXXXIV";
        Console.WriteLine("Roman Numeral: " + roman);
        Console.WriteLine("Integer: " + RomanToInt(roman));
    }

    static int RomanToInt(string s)
    {
        Dictionary<char, int> romanMap = new Dictionary<char, int>
        {
            {'I', 1 }, {'V', 5 }, {'X', 10 }, {'L', 50 },
            {'C', 100 }, {'D', 500 }, {'M', 1000 }
        };

        int total = 0;
        int prev = 0;

        for (int i = s.Length - 1; i >= 0; i--)
        {
            int current = romanMap[s[i]];
            if (current < prev)
                total -= current;
            else
                total += current;

            prev = current;
        }

        return total;
    }
}