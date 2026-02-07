using System;

class Program
{
    static void Main()
    {
        double c, f;

        Console.Write("Enter temperature in Celsius: ");
        c = Convert.ToDouble(Console.ReadLine());

        f = (c * 9 / 5) + 32;

        Console.WriteLine("Temperature in Fahrenheit = " + f);
    }
}
