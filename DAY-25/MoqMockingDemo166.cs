// Mocking using Moq


// Simulated demo — manual mock (no NuGet required)
using System;

interface IEmailSender
{
    bool Send(string to, string subject, string body);
}

// Real implementation
class SmtpEmailSender : IEmailSender
{
    public bool Send(string to, string subject, string body)
    {
        Console.WriteLine($"[SMTP] Sending to {to}: {subject}");
        return true;
    }
}

// Manual mock — controls return value, records calls
class MockEmailSender : IEmailSender
{
    public bool ReturnValue { get; set; } = true;
    public int   CallCount  { get; private set; } = 0;
    public string? LastTo   { get; private set; }

    public bool Send(string to, string subject, string body)
    {
        CallCount++;
        LastTo = to;
        return ReturnValue;
    }
}

class OrderService
{
    private readonly IEmailSender _emailSender;
    public OrderService(IEmailSender emailSender) { _emailSender = emailSender; }

    public string PlaceOrder(string customerEmail, string product)
    {
        string body = $"Your order for '{product}' has been placed.";
        bool sent = _emailSender.Send(customerEmail, "Order Confirmation", body);
        return sent ? "Order placed and email sent." : "Order placed but email failed.";
    }
}

class MoqMockingDemo
{
    static int passed = 0, failed = 0;

    static void Assert_Equal<T>(string test, T expected, T actual)
    {
        if (Equals(expected, actual)) { Console.WriteLine($"  [PASS] {test}"); passed++; }
        else { Console.WriteLine($"  [FAIL] {test} — Expected: {expected}, Got: {actual}"); failed++; }
    }

    static void Main()
    {
        Console.WriteLine("=== Mocking with Moq (simulated) ===\n");

        // Test 1 — email succeeds
        var mock1 = new MockEmailSender { ReturnValue = true };
        var svc1  = new OrderService(mock1);
        string res1 = svc1.PlaceOrder("alice@example.com", "Laptop");
        Assert_Equal("PlaceOrder — email succeeds", "Order placed and email sent.", res1);
        Assert_Equal("Send called once",             1, mock1.CallCount);
        Assert_Equal("Sent to correct address",      "alice@example.com", mock1.LastTo ?? "");

        // Test 2 — email fails
        var mock2 = new MockEmailSender { ReturnValue = false };
        var svc2  = new OrderService(mock2);
        string res2 = svc2.PlaceOrder("bob@example.com", "Phone");
        Assert_Equal("PlaceOrder — email fails", "Order placed but email failed.", res2);

        Console.WriteLine($"\nResults: {passed} passed, {failed} failed.");
    }
}
