using System;

class Student
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}

class Program
{
    static void Main()
    {
        Student obj = new Student();
        obj.Name = "Amit";

        Console.WriteLine("Student Name: " + obj.Name);
    }
}