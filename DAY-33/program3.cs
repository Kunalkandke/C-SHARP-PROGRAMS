// Program to implement a Shallow Copy using MemberwiseClone
using System;

class Address
{
    public string City { get; set; }
    public string Street { get; set; }
}

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Address Address { get; set; }

    public Person ShallowCopy()
    {
        return (Person)this.MemberwiseClone();
    }

    public void Display()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, City: {Address.City}, Street: {Address.Street}");
    }
}

class Program
{
    static void Main()
    {
        Person original = new Person
        {
            Name = "Alice",
            Age = 28,
            Address = new Address { City = "Chicago", Street = "Lake Shore Drive" }
        };

        Person copy = original.ShallowCopy();

        Console.WriteLine("Original:");
        original.Display();

        Console.WriteLine("Copy:");
        copy.Display();

        // Modify copy to observe shallow copy effect
        copy.Address.City = "San Francisco";

        Console.WriteLine("\nAfter modifying copy's city:");

        Console.WriteLine("Original:");
        original.Display();

        Console.WriteLine("Copy:");
        copy.Display();
    }
}