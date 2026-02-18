using System;

class Program
{
    static void Main()
    {
        string str1 = "Hello";
        string str2 = "hello";

        if (str1.Equals(str2, StringComparison.OrdinalIgnoreCase))
            Console.WriteLine("Strings are equal");
        else
            Console.WriteLine("Strings are not equal");
    }
}
