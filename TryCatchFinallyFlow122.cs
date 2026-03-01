using System;

class Program
{
    static void Main()
    {
        try
        {
            int x = 10;
            int y = 0;
            int result = x / y;
            Console.WriteLine(result);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Cannot divide by zero.");
        }
        finally
        {
            Console.WriteLine("Finally block executed.");
        }
    }
}