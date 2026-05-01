// Program to chain multiple Action<T> delegates together
using System;

class Program
{
    static void Main()
    {
        Action<string> greet = msg => Console.WriteLine("Hello " + msg);
        Action<string> shout = msg => Console.WriteLine(msg.ToUpper() + "!!!");
        Action<string> ask = msg => Console.WriteLine("How are you, " + msg + "?");

        // Chain delegates
        Action<string> combinedAction = greet + shout + ask;

        combinedAction("Alice");
    }
}