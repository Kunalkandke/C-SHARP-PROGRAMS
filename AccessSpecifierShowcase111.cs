using System;

class AccessExample
{
    public string PublicVar = "Public";
    private string PrivateVar = "Private";
    protected string ProtectedVar = "Protected";

    public void ShowPrivate()
    {
        Console.WriteLine("Private: " + PrivateVar);
    }
}

class Derived : AccessExample
{
    public void ShowProtected()
    {
        Console.WriteLine("Protected: " + ProtectedVar);
    }
}

class Program
{
    static void Main()
    {
        AccessExample obj = new AccessExample();
        Console.WriteLine(obj.PublicVar);
        obj.ShowPrivate();

        Derived d = new Derived();
        d.ShowProtected();
    }
}