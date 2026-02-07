using System;

class Program
{
    static void Main()
    {
        double length, width, area;

        Console.Write("Enter length: ");
        length = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter width: ");
        width = Convert.ToDouble(Console.ReadLine());

        area = length * width;

        Console.WriteLine("Area of Rectangle = " + area);
    }
}
