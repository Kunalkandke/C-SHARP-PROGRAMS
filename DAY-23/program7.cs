// Program to demonstrate attributes 
using System;

[Obsolete("This method is obsolete")]
class Demo
{
    public void Show()
    {
        Console.WriteLine("Demo Method");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Demo d = new Demo();
        d.Show();
    }
}