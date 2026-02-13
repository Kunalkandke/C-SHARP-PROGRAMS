using System;

class Program
{
    static void Main()
    {
        var person = (Id: 1, Name: "Amit", Age: 20);

        Console.WriteLine("Id = " + person.Id);
        Console.WriteLine("Name = " + person.Name);
        Console.WriteLine("Age = " + person.Age);
    }
}
