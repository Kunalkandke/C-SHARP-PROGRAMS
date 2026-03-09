using System;

class GenericClass<T>
{
    public void Show(T value)
    {
        Console.WriteLine("Value: " + value);
    }
}

class Program
{
    static void Main()
    {
        GenericClass<int> obj1 = new GenericClass<int>();
        obj1.Show(100);

        GenericClass<string> obj2 = new GenericClass<string>();
        obj2.Show("Hello Generics");
    }
}