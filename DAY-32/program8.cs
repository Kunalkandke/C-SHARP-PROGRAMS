// Program to implement the Observer Design Pattern using C# Events
using System;

class Subject
{
    public event Action<string> Notify;

    public void UpdateState(string message)
    {
        Console.WriteLine("Subject: State changed to " + message);
        Notify?.Invoke(message);
    }
}

class Observer
{
    private string name;
    public Observer(string name)
    {
        this.name = name;
    }

    public void OnNotified(string message)
    {
        Console.WriteLine($"{name} received notification: {message}");
    }
}

class Program
{
    static void Main()
    {
        Subject subject = new Subject();

        Observer observer1 = new Observer("Observer 1");
        Observer observer2 = new Observer("Observer 2");

        subject.Notify += observer1.OnNotified;
        subject.Notify += observer2.OnNotified;

        subject.UpdateState("New State 1");
        subject.UpdateState("New State 2");
    }
}