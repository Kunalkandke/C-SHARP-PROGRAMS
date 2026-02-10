using System;

class Program
{
    static void Main()
    {
        var a = 10;          // type decided at compile time
        dynamic b = 20;      // type can change at runtime

        Console.WriteLine("var a = " + a);
        Console.WriteLine("dynamic b = " + b);

        b = "Hello";         // changing type
        Console.WriteLine("dynamic b after change = " + b);
    }
}
