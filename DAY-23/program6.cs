// Program to demonstrate reflection 
using System;
using System.Reflection;

class Student
{
    public int Id;
    public string Name;

    public void Show()
    {
        Console.WriteLine("Student Method");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Type t = typeof(Student);

        Console.WriteLine("Class Name: " + t.Name);

        FieldInfo[] fields = t.GetFields();
        foreach (FieldInfo f in fields)
        {
            Console.WriteLine("Field: " + f.Name);
        }

        MethodInfo[] methods = t.GetMethods();
        foreach (MethodInfo m in methods)
        {
            Console.WriteLine("Method: " + m.Name);
        }
    }
}