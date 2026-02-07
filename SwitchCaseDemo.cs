using System;

class Program
{
    static void Main()
    {
        int choice;

        Console.WriteLine("1. Monday");
        Console.WriteLine("2. Tuesday");
        Console.WriteLine("3. Wednesday");
        Console.Write("Enter your choice: ");
        choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine("Monday");
                break;
            case 2:
                Console.WriteLine("Tuesday");
                break;
            case 3:
                Console.WriteLine("Wednesday");
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
}
