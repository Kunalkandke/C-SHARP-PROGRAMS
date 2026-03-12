using System;

class Employee
{
    public string Name;
    public double Salary;
    public double Bonus;

    public double CalculateTotal()
    {
        return Salary + Bonus;
    }
}

class Program
{
    static void Main()
    {
        Employee emp = new Employee();

        Console.Write("Enter Employee Name: ");
        emp.Name = Console.ReadLine();

        Console.Write("Enter Salary: ");
        emp.Salary = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter Bonus: ");
        emp.Bonus = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Total Salary: " + emp.CalculateTotal());
    }
}