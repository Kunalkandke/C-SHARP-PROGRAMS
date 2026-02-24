using System;

class Program
{
    static void Main()
    {
        string str = "Hello";
        str.Replace("H", "Y");

        Console.WriteLine("Original String: " + str);

        str = str.Replace("H", "Y");
        Console.WriteLine("Modified String: " + str);
    }
}