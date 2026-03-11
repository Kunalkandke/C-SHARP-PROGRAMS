// Program to demonstrate serialization (JSON) 
using System;
using System.Text.Json;

class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        Student s = new Student { Id = 1, Name = "Rahul" };

        string json = JsonSerializer.Serialize(s);
        Console.WriteLine(json);

        Student s2 = JsonSerializer.Deserialize<Student>(json);
        Console.WriteLine(s2.Id + " " + s2.Name);
    }
}