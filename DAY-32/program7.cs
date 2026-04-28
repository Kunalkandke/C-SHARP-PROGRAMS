// Program to implement the Factory Method Design Pattern
using System;

abstract class Product
{
    public abstract void Display();
}

class ConcreteProductA : Product
{
    public override void Display()
    {
        Console.WriteLine("This is Product A");
    }
}

class ConcreteProductB : Product
{
    public override void Display()
    {
        Console.WriteLine("This is Product B");
    }
}

abstract class Creator
{
    public abstract Product FactoryMethod();
}

class ConcreteCreatorA : Creator
{
    public override Product FactoryMethod()
    {
        return new ConcreteProductA();
    }
}

class ConcreteCreatorB : Creator
{
    public override Product FactoryMethod()
    {
        return new ConcreteProductB();
    }
}

class Program
{
    static void Main()
    {
        Creator creatorA = new ConcreteCreatorA();
        Product productA = creatorA.FactoryMethod();
        productA.Display();

        Creator creatorB = new ConcreteCreatorB();
        Product productB = creatorB.FactoryMethod();
        productB.Display();
    }
}