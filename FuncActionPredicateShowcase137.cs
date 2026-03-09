using System;

class Program
{
    static void Main()
    {
        Func<int, int, int> add = (a, b) => a + b;
        Action<string> print = msg => Console.WriteLine(msg);
        Predicate<int> isEven = num => num % 2 == 0;

        Console.WriteLine(add(4, 6));
        print("Hello Action");
        Console.WriteLine(isEven(10));
    }
}