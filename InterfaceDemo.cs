using System;

interface IAnimal
{
    void Speak();
}

class Dog : IAnimal
{
    public void Speak()
    {
        Console.WriteLine("Dog Barks");
    }
}

class Program
{
    static void Main()
    {
        IAnimal obj = new Dog();
        obj.Speak();
    }
}