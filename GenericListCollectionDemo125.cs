using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> students = new List<string>();

        students.Add("Rahul");
        students.Add("Priya");
        students.Add("Sneha");

        foreach (string name in students)
        {
            Console.WriteLine(name);
        }
    }
}