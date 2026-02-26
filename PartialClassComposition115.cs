using System;

partial class PartialDemo
{
    public void Method1()
    {
        Console.WriteLine("Method 1 from first part");
    }
}

partial class PartialDemo
{
    public void Method2()
    {
        Console.WriteLine("Method 2 from second part");
    }
}

class Program
{
    static void Main()
    {
        PartialDemo obj = new PartialDemo();
        obj.Method1();
        obj.Method2();
    }
}