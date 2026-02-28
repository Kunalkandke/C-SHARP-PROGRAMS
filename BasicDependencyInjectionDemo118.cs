using System;

interface IMessageService
{
    void SendMessage(string message);
}

class EmailService : IMessageService
{
    public void SendMessage(string message)
    {
        Console.WriteLine("Email Sent: " + message);
    }
}

class Notification
{
    private readonly IMessageService _service;

    public Notification(IMessageService service)
    {
        _service = service;
    }

    public void Notify(string msg)
    {
        _service.SendMessage(msg);
    }
}

class Program
{
    static void Main()
    {
        IMessageService service = new EmailService();
        Notification notification = new Notification(service);
        notification.Notify("Hello DI");
    }
}