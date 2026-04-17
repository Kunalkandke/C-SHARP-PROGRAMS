// Program to generate all Permutations of a given string
using System;

class Program
{
    static void Main()
    {
        string str = "ABC";
        Console.WriteLine("Permutations of " + str + ":");
        GeneratePermutations(str, 0, str.Length - 1);
    }

    static void GeneratePermutations(string str, int l, int r)
    {
        if (l == r)
        {
            Console.WriteLine(str);
        }
        else
        {
            for (int i = l; i <= r; i++)
            {
                str = Swap(str, l, i);
                GeneratePermutations(str, l + 1, r);
                str = Swap(str, l, i); // backtrack
            }
        }
    }

    static string Swap(string str, int i, int j)
    {
        char[] charArray = str.ToCharArray();
        char temp = charArray[i];
        charArray[i] = charArray[j];
        charArray[j] = temp;
        return new string(charArray);
    }
}