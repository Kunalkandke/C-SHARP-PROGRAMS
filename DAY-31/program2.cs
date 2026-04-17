// 222. Program to perform String Compression (e.g., convert "aabcccccaaa" to "a2b1c5a3")
using System;
using System.Text;

class Program
{
    static void Main()
    {
        string input = "aabcccccaaa";
        StringBuilder compressed = new StringBuilder();
        int count = 1;

        for (int i = 1; i <= input.Length; i++)
        {
            if (i < input.Length && input[i] == input[i - 1])
            {
                count++;
            }
            else
            {
                compressed.Append(input[i - 1]);
                compressed.Append(count);
                count = 1;
            }
        }

        string result = compressed.Length < input.Length ? compressed.ToString() : input;
        Console.WriteLine("Compressed string: " + result);
    }
}