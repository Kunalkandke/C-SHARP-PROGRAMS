using System;

abstract class Vehicle
{
    public abstract void Start();
    public void Stop()
    {
        Console.WriteLine("Vehicle Stopped");
    }
}

interface IDrive
{
    void Drive();
}

class Car : Vehicle, IDrive
{
    public override void Start()
    {
        Console.WriteLine("Car Started");
    }

    public void Drive()
    {
        Console.WriteLine("Car Driving");
    }
}

class Program
{
    static void Main()
    {
        Car obj = new Car();
        obj.Start();
        obj.Drive();
        obj.Stop();
    }
}