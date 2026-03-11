// Console-based Student Management System 
using System;
using System.Collections.Generic;

class Student
{
    public int Id;
    public string Name;
    public int Age;
}

class Program
{
    static List<Student> students = new List<Student>();

    static void AddStudent()
    {
        Student s = new Student();

        Console.Write("Enter ID: ");
        s.Id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Name: ");
        s.Name = Console.ReadLine();

        Console.Write("Enter Age: ");
        s.Age = Convert.ToInt32(Console.ReadLine());

        students.Add(s);
    }

    static void ShowStudents()
    {
        foreach (Student s in students)
        {
            Console.WriteLine(s.Id + " " + s.Name + " " + s.Age);
        }
    }

    static void Main(string[] args)
    {
        int choice;

        do
        {
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Show Students");
            Console.WriteLine("3. Exit");

            Console.Write("Enter choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
                AddStudent();
            else if (choice == 2)
                ShowStudents();

        } while (choice != 3);
    }
}