using System;

class Program
{
    static void Main()
    {
        double radius, area;

        Console.Write("Enter radius: ");
        radius = Convert.ToDouble(Console.ReadLine());

        area = Math.PI * radius * radius;

        Console.WriteLine("Area of Circle = " + area);
    }
}
