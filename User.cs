using System;

class Program
{
    static void Main()
    {
        string name;
        int age;

        Console.Write("Enter Name: ");
        name = Console.ReadLine();

        Console.Write("Enter Age: ");
        age = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Name: " + name);
        Console.WriteLine("Age: " + age);
    }
}
