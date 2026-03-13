// Dependency Injection in Console App


// Simulated demo (no NuGet required) — manual DI container pattern
using System;
using System.Collections.Generic;

interface IGreetingService
{
    void Greet(string name);
}

class GreetingService : IGreetingService
{
    public void Greet(string name) =>
        Console.WriteLine($"Hello, {name}! Welcome to Dependency Injection.");
}

class EmailService : IGreetingService
{
    public void Greet(string name) =>
        Console.WriteLine($"Email sent to {name}: Welcome aboard!");
}

// Consumer class — receives dependency via constructor (Constructor Injection)
class App
{
    private readonly IGreetingService _service;

    public App(IGreetingService service) { _service = service; }

    public void Run(string[] names)
    {
        foreach (var name in names)
            _service.Greet(name);
    }
}

// Minimal manual DI container
class SimpleContainer
{
    private readonly Dictionary<Type, Func<object>> _registrations = new();

    public void Register<TInterface, TImpl>() where TImpl : TInterface, new()
        => _registrations[typeof(TInterface)] = () => new TImpl();

    public T Resolve<T>() => (T)_registrations[typeof(T)]();
}

class DependencyInjectionConsoleDemo
{
    static void Main()
    {
        Console.WriteLine("=== Dependency Injection in Console App ===\n");

        var container = new SimpleContainer();
        container.Register<IGreetingService, GreetingService>();

        // Resolve and run
        var service = container.Resolve<IGreetingService>();
        var app = new App(service);
        app.Run(new[] { "Alice", "Bob", "Charlie" });

        // Swap implementation — no change needed in App class
        Console.WriteLine("\n[Swapping to EmailService]\n");
        container.Register<IGreetingService, EmailService>();
        var app2 = new App(container.Resolve<IGreetingService>());
        app2.Run(new[] { "Dave", "Eve" });
    }
}
