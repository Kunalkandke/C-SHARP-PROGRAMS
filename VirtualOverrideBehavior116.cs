using System;

class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Animal makes sound");
    }
}

class Cat : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Cat says Meow");
    }
}

class Program
{
    static void Main()
    {
        Animal obj = new Cat();
        obj.Sound();
    }
}