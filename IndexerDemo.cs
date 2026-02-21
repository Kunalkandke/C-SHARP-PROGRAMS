using System;

class Sample
{
    private string[] names = new string[3];

    public string this[int index]
    {
        get { return names[index]; }
        set { names[index] = value; }
    }
}

class Program
{
    static void Main()
    {
        Sample obj = new Sample();

        obj[0] = "Amit";
        obj[1] = "Rahul";
        obj[2] = "Priya";

        Console.WriteLine(obj[0]);
        Console.WriteLine(obj[1]);
        Console.WriteLine(obj[2]);
    }
}