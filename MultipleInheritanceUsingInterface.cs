using System;

interface IFather
{
    void ShowFather();
}

interface IMother
{
    void ShowMother();
}

class Child : IFather, IMother
{
    public void ShowFather()
    {
        Console.WriteLine("Father's Features");
    }

    public void ShowMother()
    {
        Console.WriteLine("Mother's Features");
    }
}

class Program
{
    static void Main()
    {
        Child obj = new Child();
        obj.ShowFather();
        obj.ShowMother();
    }
}