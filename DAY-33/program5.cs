// Program to overload equality operators (==, !=) and override Equals()
using System;

class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Person other = (Person)obj;
        return Name == other.Name && Age == other.Age;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Age);
    }

    public static bool operator ==(Person p1, Person p2)
    {
        if (ReferenceEquals(p1, p2))
            return true;
        if (p1 is null || p2 is null)
            return false;
        return p1.Equals(p2);
    }

    public static bool operator !=(Person p1, Person p2)
    {
        return !(p1 == p2);
    }
}

class Program
{
    static void Main()
    {
        Person p1 = new Person { Name = "Alice", Age = 30 };
        Person p2 = new Person { Name = "Alice", Age = 30 };
        Person p3 = new Person { Name = "Bob", Age = 25 };

        Console.WriteLine("p1 == p2: " + (p1 == p2));
        Console.WriteLine("p1 != p3: " + (p1 != p3));
        Console.WriteLine("p1.Equals(p3): " + p1.Equals(p3));
    }
}