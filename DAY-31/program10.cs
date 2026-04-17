// Program to find the Longest Palindromic Substring within a string
using System;

class Program
{
    static void Main()
    {
        string s = "babad";
        Console.WriteLine("Input string: " + s);
        Console.WriteLine("Longest Palindromic Substring: " + LongestPalindrome(s));
    }

    static string LongestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s)) return "";

        int start = 0, maxLength = 1;

        for (int i = 0; i < s.Length; i++)
        {
            ExpandAroundCenter(s, i, i, ref start, ref maxLength);   // Odd length palindrome
            ExpandAroundCenter(s, i, i + 1, ref start, ref maxLength); // Even length palindrome
        }

        return s.Substring(start, maxLength);
    }

    static void ExpandAroundCenter(string s, int left, int right, ref int start, ref int maxLength)
    {
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            if (right - left + 1 > maxLength)
            {
                start = left;
                maxLength = right - left + 1;
            }
            left--;
            right++;
        }
    }
}