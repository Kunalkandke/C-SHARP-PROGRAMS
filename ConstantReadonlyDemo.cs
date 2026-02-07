using System;

class Program
{
    const double PI = 3.14;   // constant

    readonly int value;

    public Program()
    {
        value = 100;   // readonly set in constructor
    }

    static void Main()
    {
        Program obj = new Program();

        Console.WriteLine("Constant PI = " + PI);
        Console.WriteLine("Readonly value = " + obj.value);
    }
}
