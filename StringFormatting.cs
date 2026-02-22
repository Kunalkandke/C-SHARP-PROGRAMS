using System;

class Program
{
    static void Main()
    {
        string name = "Amit";
        int age = 21;
        double marks = 85.5678;

        Console.WriteLine("Name: {0}, Age: {1}", name, age);
        Console.WriteLine("Marks (2 decimal places): {0:F2}", marks);
        Console.WriteLine("Currency Format: {0:C}", 1500);
    }
}