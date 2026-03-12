using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Role (Admin/User): ");
        string role = Console.ReadLine();

        if (role == "Admin")
        {
            Console.WriteLine("Access to Admin Dashboard");
        }
        else if (role == "User")
        {
            Console.WriteLine("Access to User Dashboard");
        }
        else
        {
            Console.WriteLine("Access Denied");
        }
    }
}