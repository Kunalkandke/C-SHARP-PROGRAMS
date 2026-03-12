using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Mini ERP System");

        Console.WriteLine("1. HR Module");
        Console.WriteLine("2. Sales Module");
        Console.WriteLine("3. Inventory Module");

        Console.Write("Select Module: ");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine("HR Module Selected");
                break;

            case 2:
                Console.WriteLine("Sales Module Selected");
                break;

            case 3:
                Console.WriteLine("Inventory Module Selected");
                break;

            default:
                Console.WriteLine("Invalid Choice");
                break;
        }
    }
}