using System;

class Parent
{
    public void Show()
    {
        Console.WriteLine("Parent Class Method");
    }
}

class Child : Parent
{
    public void Display()
    {
        Console.WriteLine("Child Class Method");
    }
}

class Program
{
    static void Main()
    {
        Child obj = new Child();
        obj.Show();
        obj.Display();
    }
}