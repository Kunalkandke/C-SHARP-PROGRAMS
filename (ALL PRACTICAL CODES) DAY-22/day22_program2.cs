using System;

class Program
{
    static void Main()
    {
        int choice;

        do
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. Check Even or Odd");
            Console.WriteLine("2. Display Multiplication Table");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Even or Odd using if
                    Console.Write("Enter a number: ");
                    int num = Convert.ToInt32(Console.ReadLine());

                    if (num % 2 == 0)
                    {
                        Console.WriteLine(num + " is an Even number.");
                    }
                    else
                    {
                        Console.WriteLine(num + " is an Odd number.");
                    }
                    break;

                case 2:
                    // Multiplication table using for loop
                    Console.Write("Enter a number for multiplication table: ");
                    int tableNum = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("\nMultiplication Table using FOR loop:");
                    for (int i = 1; i <= 10; i++)
                    {
                        Console.WriteLine(tableNum + " x " + i + " = " + (tableNum * i));
                    }

                    // Multiplication table using while loop
                    Console.WriteLine("\nMultiplication Table using WHILE loop:");
                    int j = 1;
                    while (j <= 10)
                    {
                        Console.WriteLine(tableNum + " x " + j + " = " + (tableNum * j));
                        j++;
                    }
                    break;

                case 3:
                    Console.WriteLine("Exiting the program...");
                    break;

                default:
                    Console.WriteLine("Invalid choice! Please try again.");
                    break;
            }

        } while (choice != 3); // do-while loop

        Console.WriteLine("Program ended.");
    }
}
