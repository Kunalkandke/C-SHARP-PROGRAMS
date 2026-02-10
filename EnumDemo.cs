using System;

class Program
{
    enum Days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday }

    static void Main()
    {
        Days today = Days.Monday;

        Console.WriteLine("Today is: " + today);
        Console.WriteLine("Value = " + (int)today);
    }
}
