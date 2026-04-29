// Program to implement a Deep Copy of an object using ICloneable
using System;

class Address : ICloneable
{
    public string City { get; set; }
    public string Street { get; set; }

    public object Clone()
    {
        return new Address { City = this.City, Street = this.Street };
    }
}

class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Address Address { get; set; }

    public object Clone()
    {
        return new Person
        {
            Name = this.Name,
            Age = this.Age,
            Address = (Address)this.Address.Clone()
        };
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
            Name = "John",
            Age = 30,
            Address = new Address { City = "New York", Street = "5th Avenue" }
        };

        Person copy = (Person)original.Clone();

        Console.WriteLine("Original:");
        original.Display();

        Console.WriteLine("Copy:");
        copy.Display();

        // Modify copy to verify deep copy
        copy.Address.City = "Los Angeles";
        Console.WriteLine("\nAfter modifying copy's city:");

        Console.WriteLine("Original:");
        original.Display();

        Console.WriteLine("Copy:");
        copy.Display();
    }
}