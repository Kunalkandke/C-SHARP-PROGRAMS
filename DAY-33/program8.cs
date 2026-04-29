// Program to demonstrate Covariant (out) and Contravariant (in) interfaces
using System;
using System.Collections.Generic;

// Covariant interface (out)
interface ICovariant<out T>
{
    T GetItem();
}

// Contravariant interface (in)
interface IContravariant<in T>
{
    void SetItem(T item);
}

class Animal
{
    public virtual void Speak() => Console.WriteLine("Animal sound");
}

class Dog : Animal
{
    public override void Speak() => Console.WriteLine("Dog barks");
}

class AnimalProvider : ICovariant<Animal>
{
    private Animal animal;
    public AnimalProvider(Animal a) => animal = a;
    public Animal GetItem() => animal;
}

class AnimalConsumer : IContravariant<Animal>
{
    public void SetItem(Animal a) => a.Speak();
}

class Program
{
    static void Main()
    {
        // Covariance: ICovariant<Dog> can be assigned to ICovariant<Animal>
        ICovariant<Dog> dogProvider = new AnimalProvider(new Dog()) as ICovariant<Dog>;
        ICovariant<Animal> animalProvider = dogProvider;
        Animal a = animalProvider.GetItem();
        a.Speak();

        // Contravariance: IContravariant<Animal> can be assigned to IContravariant<Dog>
        IContravariant<Animal> animalConsumer = new AnimalConsumer();
        IContravariant<Dog> dogConsumer = animalConsumer;
        dogConsumer.SetItem(new Dog());
    }
}