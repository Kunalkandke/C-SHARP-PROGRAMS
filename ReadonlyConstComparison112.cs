using System;

class ConstantDemo
{
    public const double PI = 3.14;
    public readonly int Number;

    public ConstantDemo(int num)
    {
        Number = num;
    }
}

class Program
{
    static void Main()
    {
        ConstantDemo obj = new ConstantDemo(100);

        Console.WriteLine("Const PI: " + ConstantDemo.PI);
        Console.WriteLine("Readonly Number: " + obj.Number);
    }
}