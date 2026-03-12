using System;
using System.Collections.Generic;

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>();

        students.Add(new Student { Id = 1, Name = "Amit" });
        students.Add(new Student { Id = 2, Name = "Priya" });

        foreach (var s in students)
        {
            Console.WriteLine(s.Id + " " + s.Name);
        }
    }
}