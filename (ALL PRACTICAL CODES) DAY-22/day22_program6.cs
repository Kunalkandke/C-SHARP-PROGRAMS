using System;

// Base class
class Shape
{
    // Virtual method
    public virtual double CalculateArea()
    {
        return 0;
    }
}

// Derived class Circle
class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    // Overriding base class method
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

// Derived class Rectangle
class Rectangle : Shape
{
    public double Length { get; set; }
    public double Breadth { get; set; }

    public Rectangle(double length, double breadth)
    {
        Length = length;
        Breadth = breadth;
    }

    // Overriding base class method
    public override double CalculateArea()
    {
        return Length * Breadth;
    }
}

class Program
{
    static void Main()
    {
        // Creating Circle object
        Shape circle = new Circle(5);
        Console.WriteLine("Area of Circle: " + circle.CalculateArea());

        // Creating Rectangle object
        Shape rectangle = new Rectangle(4, 6);
        Console.WriteLine("Area of Rectangle: " + rectangle.CalculateArea());
    }
}
