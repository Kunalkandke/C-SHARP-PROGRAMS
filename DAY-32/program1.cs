// Program to implement Run-Length Encoding for string data
using System;
using System.Text;

class Program
{
    static void Main()
    {
        string input = "aaabbcdddde";
        Console.WriteLine("Input string: " + input);
        Console.WriteLine("Run-Length Encoded string: " + RunLengthEncode(input));
    }

    static string RunLengthEncode(string str)
    {
        if (string.IsNullOrEmpty(str)) return "";

        StringBuilder encoded = new StringBuilder();
        int count = 1;

        for (int i = 1; i <= str.Length; i++)
        {
            if (i < str.Length && str[i] == str[i - 1])
            {
                count++;
            }
            else
            {
                encoded.Append(str[i - 1]);
                encoded.Append(count);
                count = 1;
            }
        }

        return encoded.ToString();
    }
}
