using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string str = Console.ReadLine();

        char[] arr = str.ToCharArray();
        Array.Reverse(arr);

        string rev = new string(arr);

        Console.WriteLine("Reversed string = " + rev);
    }
}
