// Unit Testing using xUnit


// Simulated demo — manual assertion runner (no NuGet required)
using System;

class Calculator
{
    public int    Add(int a, int b)      => a + b;
    public int    Subtract(int a, int b) => a - b;
    public int    Multiply(int a, int b) => a * b;
    public double Divide(int a, int b)
    {
        if (b == 0) throw new DivideByZeroException("Cannot divide by zero.");
        return (double)a / b;
    }
}

class XUnitTestingDemo
{
    static int passed = 0, failed = 0;

    static void Assert_Equal<T>(string testName, T expected, T actual)
    {
        if (Equals(expected, actual)) { Console.WriteLine($"  [PASS] {testName}"); passed++; }
        else { Console.WriteLine($"  [FAIL] {testName} — Expected: {expected}, Got: {actual}"); failed++; }
    }

    static void Assert_Throws<TException>(string testName, Action action) where TException : Exception
    {
        try { action(); Console.WriteLine($"  [FAIL] {testName} — Expected exception not thrown"); failed++; }
        catch (TException) { Console.WriteLine($"  [PASS] {testName}"); passed++; }
    }

    static void Main()
    {
        Console.WriteLine("=== Unit Testing with xUnit (simulated) ===\n");
        var calc = new Calculator();

        Assert_Equal("Add(3,4) == 7",              7,    calc.Add(3, 4));
        Assert_Equal("Subtract(5,4) == 1",         1,    calc.Subtract(5, 4));
        Assert_Equal("Multiply(2,3) == 6",         6,    calc.Multiply(2, 3));
        Assert_Equal("Multiply(0,100) == 0",       0,    calc.Multiply(0, 100));
        Assert_Equal("Multiply(-1,5) == -5",      -5,    calc.Multiply(-1, 5));
        Assert_Equal("Divide(10,4) == 2.5",       2.5,   calc.Divide(10, 4));
        Assert_Throws<DivideByZeroException>("Divide(10,0) throws", () => calc.Divide(10, 0));

        Console.WriteLine($"\nResults: {passed} passed, {failed} failed.");
    }
}
