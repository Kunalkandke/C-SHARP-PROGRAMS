using System;

class ExceptionDemo
{
    static void Main()
    {
        try
        {
            Console.Write("Enter first number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter second number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            int result = num1 / num2;   // May cause divide-by-zero

            Console.WriteLine("Result = " + result);
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Error: Division by zero is not allowed.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Please enter valid integer numbers.");
        }
        catch (Exception)
        {
            Console.WriteLine("An unexpected error occurred.");
        }
        finally
        {
            Console.WriteLine("Program execution completed.");
        }
    }
}
