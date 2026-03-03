using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<int, string> students = new Dictionary<int, string>();

        students.Add(1, "Amit");
        students.Add(2, "Priya");
        students.Add(3, "Rahul");

        foreach (var item in students)
        {
            Console.WriteLine("Roll No: " + item.Key + " Name: " + item.Value);
        }
    }
}