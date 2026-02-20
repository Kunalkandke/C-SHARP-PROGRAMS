using System;

class Program
{
    static void Main()
    {
        string str1 = "listen";
        string str2 = "silent";

        char[] arr1 = str1.ToLower().ToCharArray();
        char[] arr2 = str2.ToLower().ToCharArray();

        Array.Sort(arr1);
        Array.Sort(arr2);

        if (new string(arr1) == new string(arr2))
            Console.WriteLine("Anagram");
        else
            Console.WriteLine("Not Anagram");
    }
}