using System;

delegate void Notify();

class Program
{
    static void Message1()
    {
        Console.WriteLine("Message 1");
    }

    static void Message2()
    {
        Console.WriteLine("Message 2");
    }

    static void Main()
    {
        Notify notify = Message1;
        notify += Message2;

        notify();
    }
}