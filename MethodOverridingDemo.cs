using System;

class Parent
{
    public virtual void Show()
    {
        Console.WriteLine("Parent Method");
    }
}

class Child : Parent
{
    public override void Show()
    {
        Console.WriteLine("Child Method");
    }
}

class Program
{
    static void Main()
    {
        Parent obj = new Child();
        obj.Show();
    }
}