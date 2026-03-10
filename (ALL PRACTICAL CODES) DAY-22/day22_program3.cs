using System;

class Calculator
{
    // User-defined method for Addition
    static int Add(int a, int b)
    {
        return a + b;
    }

    // User-defined method for Subtraction
    static int Subtract(int a, int b)
    {
        return a - b;
    }

    // User-defined method for Multiplication
    static int Multiply(int a, int b)
    {
        return a * b;
    }

    // User-defined method for Division
    static double Divide(int a, int b)
    {
        if (b != 0)
            return (double)a / b;
        else
        {
            Console.WriteLine("Division by zero is not allowed.");
            return 0;
        }
    }

    static void Main()
    {
        int choice, num1, num2;

        Console.Write("Enter first number: ");
        num1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        num2 = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("\n--- Calculator Menu ---");
        Console.WriteLine("1. Addition");
        Console.WriteLine("2. Subtraction");
        Console.WriteLine("3. Multiplication");
        Console.WriteLine("4. Division");
        Console.Write("Enter your choice: ");
        choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine("Result = " + Add(num1, num2));
                break;

            case 2:
                Console.WriteLine("Result = " + Subtract(num1, num2));
                break;

            case 3:
                Console.WriteLine("Result = " + Multiply(num1, num2));
                break;

            case 4:
                Console.WriteLine("Result = " + Divide(num1, num2));
                break;

            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }
}
