using System;

class Program
{
    static void Main()
    {
        double num, power, result;

        Console.Write("Enter number: ");
        num = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter power: ");
        power = Convert.ToDouble(Console.ReadLine());

        result = Math.Pow(num, power);

        Console.WriteLine("Result = " + result);
    }
}
