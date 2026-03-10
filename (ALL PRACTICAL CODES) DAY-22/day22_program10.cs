using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public int RollNo { get; set; }
    public string Name { get; set; }
    public int Marks { get; set; }
}

class Program
{
    static void Main()
    {
        // Creating a List to store student records
        List<Student> students = new List<Student>()
        {
            new Student { RollNo = 1, Name = "Amit", Marks = 78 },
            new Student { RollNo = 2, Name = "Neha", Marks = 85 },
            new Student { RollNo = 3, Name = "Rahul", Marks = 62 },
            new Student { RollNo = 4, Name = "Sneha", Marks = 90 }
        };

        // Display all students
        Console.WriteLine("All Student Records:");
        foreach (var s in students)
        {
            Console.WriteLine($"{s.RollNo}  {s.Name}  {s.Marks}");
        }

        // ---------- Search using LINQ ----------
        Console.Write("\nEnter Roll Number to search: ");
        int rno = Convert.ToInt32(Console.ReadLine());

        var searchResult = students
                           .Where(s => s.RollNo == rno)
                           .FirstOrDefault();

        if (searchResult != null)
        {
            Console.WriteLine("Student Found:");
            Console.WriteLine($"{searchResult.RollNo}  {searchResult.Name}  {searchResult.Marks}");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }

        // ---------- Filter using LINQ ----------
        Console.WriteLine("\nStudents with Marks >= 75:");
        var filteredStudents = students
                               .Where(s => s.Marks >= 75)
                               .ToList();

        foreach (var s in filteredStudents)
        {
            Console.WriteLine($"{s.RollNo}  {s.Name}  {s.Marks}");
        }
    }
}
