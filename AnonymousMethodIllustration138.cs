using System;

delegate void Greeting();

class Program
{
    static void Main()
    {
        Greeting greet = delegate ()
        {
            Console.WriteLine("Hello from Anonymous Method");
        };

        greet();
    }
}