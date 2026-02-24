using System;

class Student
{
    string name;

    public Student(string n)
    {
        name = n;
    }

    public void Display()
    {
        Console.WriteLine("Name: " + name);
    }
}

class Program
{
    static void Main()
    {
        Student s = new Student("Rahul");
        s.Display();
    }
}