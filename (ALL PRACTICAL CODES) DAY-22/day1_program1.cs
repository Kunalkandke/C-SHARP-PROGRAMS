using System;

namespace DataTypesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Integer data type
            int age = 20;

            // Float data type
            float height = 5.6f;

            // Double data type
            double salary = 35000.75;

            // Boolean data type
            bool isStudent = true;

            // Character data type
            char grade = 'A';

            // String data type
            string name = "Data Analyst";

            // Displaying the values
            Console.WriteLine("---- C# Data Types Demonstration ----");
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Age: " + age);
            Console.WriteLine("Height: " + height);
            Console.WriteLine("Salary: " + salary);
            Console.WriteLine("Is Student: " + isStudent);
            Console.WriteLine("Grade: " + grade);

            // Pause the console
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
