// Program to build a simple State Machine using classes
using System;

abstract class State
{
    public abstract void Handle(Context context);
}

class StartState : State
{
    public override void Handle(Context context)
    {
        Console.WriteLine("State: Start");
        context.State = new ProcessingState();
    }
}

class ProcessingState : State
{
    public override void Handle(Context context)
    {
        Console.WriteLine("State: Processing");
        context.State = new EndState();
    }
}

class EndState : State
{
    public override void Handle(Context context)
    {
        Console.WriteLine("State: End");
    }
}

class Context
{
    public State State { get; set; }

    public Context(State state)
    {
        State = state;
    }

    public void Request()
    {
        State.Handle(this);
    }
}

class Program
{
    static void Main()
    {
        Context context = new Context(new StartState());

        context.Request(); // Start -> Processing
        context.Request(); // Processing -> End
        context.Request(); // End
    }
}