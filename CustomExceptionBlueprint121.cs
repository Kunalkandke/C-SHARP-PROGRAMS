using System;

class InvalidAgeException : Exception
{
    public InvalidAgeException(string message) : base(message) { }
}

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Enter age: ");
            int age = int.Parse(Console.ReadLine());

            if (age < 18)
                throw new InvalidAgeException("Age must be 18 or above.");

            Console.WriteLine("Valid Age");
        }
        catch (InvalidAgeException ex)
        {
            Console.WriteLine("Custom Exception: " + ex.Message);
        }
    }
}