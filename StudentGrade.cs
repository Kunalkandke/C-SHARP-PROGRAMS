using System;

class Program
{
    static void Main()
    {
        int marks;

        Console.Write("Enter marks: ");
        marks = Convert.ToInt32(Console.ReadLine());

        if (marks >= 75)
            Console.WriteLine("Grade A");
        else if (marks >= 60)
            Console.WriteLine("Grade B");
        else if (marks >= 50)
            Console.WriteLine("Grade C");
        else
            Console.WriteLine("Fail");
    }
}
