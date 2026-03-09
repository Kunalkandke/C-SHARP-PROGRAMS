using System;

delegate void ShowMessage();

class Program
{
    static void Display()
    {
        Console.WriteLine("Hello from Delegate");
    }

    static void Main()
    {
        ShowMessage msg = Display;
        msg();
    }
}