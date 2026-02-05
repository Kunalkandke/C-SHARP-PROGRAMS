using System;

class Program
{
    static void Main()
    {
        float p, r, t, si;

        Console.Write("Enter Principal: ");
        p = Convert.ToSingle(Console.ReadLine());

        Console.Write("Enter Rate: ");
        r = Convert.ToSingle(Console.ReadLine());

        Console.Write("Enter Time: ");
        t = Convert.ToSingle(Console.ReadLine());

        si = (p * r * t) / 100;
        Console.WriteLine("Simple Interest = " + si);
    }
}
