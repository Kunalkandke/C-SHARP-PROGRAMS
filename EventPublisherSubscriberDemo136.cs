using System;

class Publisher
{
    public event Action Notify;

    public void Process()
    {
        Console.WriteLine("Process Started");
        Notify?.Invoke();
    }
}

class Program
{
    static void Main()
    {
        Publisher pub = new Publisher();

        pub.Notify += () => Console.WriteLine("Event Triggered!");

        pub.Process();
    }
}