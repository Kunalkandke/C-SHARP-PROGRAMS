using System;

class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }
}

class Program
{
    static void Main()
    {
        Calculator obj = new Calculator();

        Console.WriteLine("Integer Add: " + obj.Add(5, 10));
        Console.WriteLine("Double Add: " + obj.Add(5.5, 2.3));
    }
}