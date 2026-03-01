using System;

class Program
{
    static void CheckNumber(int number)
    {
        if (number < 0)
            throw new ArgumentException("Number cannot be negative.");
    }

    static void Main()
    {
        try
        {
            Console.Write("Enter number: ");
            int num = int.Parse(Console.ReadLine());

            CheckNumber(num);
            Console.WriteLine("Valid Number");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
    }
}