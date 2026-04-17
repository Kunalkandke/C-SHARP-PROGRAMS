// Program to convert an integer into Roman Numerals
using System;

class Program
{
    static void Main()
    {
        int number = 1987;
        Console.WriteLine("Integer: " + number);
        Console.WriteLine("Roman Numeral: " + ToRoman(number));
    }

    static string ToRoman(int num)
    {
        string[] thousands = { "", "M", "MM", "MMM" };
        string[] hundreds  = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        string[] tens      = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        string[] ones      = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

        return thousands[num / 1000] +
               hundreds[(num % 1000) / 100] +
               tens[(num % 100) / 10] +
               ones[num % 10];
    }
}