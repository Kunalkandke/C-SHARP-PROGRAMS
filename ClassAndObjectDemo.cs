using System;

class Student
{
    public string name;
    public int age;

    public void Display()
    {
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Age: " + age);
    }
}

class Program
{
    static void Main()
    {
        Student s1 = new Student();
        s1.name = "Amit";
        s1.age = 20;
        s1.Display();
    }
}