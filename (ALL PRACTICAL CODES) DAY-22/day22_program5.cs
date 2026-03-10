using System;

class Student
{
    // Properties
    public int RollNo { get; set; }
    public string Name { get; set; }
    public string Course { get; set; }

    // Default Constructor
    public Student()
    {
        RollNo = 0;
        Name = "Not Assigned";
        Course = "Not Assigned";
    }

    // Parameterized Constructor
    public Student(int rollNo, string name, string course)
    {
        RollNo = rollNo;
        Name = name;
        Course = course;
    }

    // Method to display student details
    public void Display()
    {
        Console.WriteLine("\nStudent Details:");
        Console.WriteLine("Roll Number : " + RollNo);
        Console.WriteLine("Name        : " + Name);
        Console.WriteLine("Course      : " + Course);
    }
}

class Program
{
    static void Main()
    {
        // Taking input from user
        Console.Write("Enter Roll Number: ");
        int rno = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Course: ");
        string course = Console.ReadLine();

        // Creating object using parameterized constructor
        Student s1 = new Student(rno, name, course);

        // Displaying student details
        s1.Display();
    }
}
