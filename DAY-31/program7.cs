// Program to check if one string is a Rotation of another
using System;

class Program
{
    static bool IsRotation(string s1, string s2)
    {
        if (s1.Length != s2.Length)
            return false;
        string combined = s1 + s1;
        return combined.Contains(s2);
    }

    static void Main()
    {
        string str1 = "ABCD";
        string str2 = "CDAB";

        Console.WriteLine($"Is \"{str2}\" a rotation of \"{str1}\": " + IsRotation(str1, str2));
    }
}