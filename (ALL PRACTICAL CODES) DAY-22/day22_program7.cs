using System;

// Interface
interface IEmployee
{
    void CalculateSalary();
}

// Class 1: Permanent Employee
class PermanentEmployee : IEmployee
{
    public double BasicSalary { get; set; }

    public PermanentEmployee(double salary)
    {
        BasicSalary = salary;
    }

    public void CalculateSalary()
    {
        double hra = BasicSalary * 0.20;
        double da = BasicSalary * 0.10;
        double totalSalary = BasicSalary + hra + da;

        Console.WriteLine("Permanent Employee Salary: " + totalSalary);
    }
}

// Class 2: Contract Employee
class ContractEmployee : IEmployee
{
    public double HourlyRate { get; set; }
    public int HoursWorked { get; set; }

    public ContractEmployee(double rate, int hours)
    {
        HourlyRate = rate;
        HoursWorked = hours;
    }

    public void CalculateSalary()
    {
        double totalSalary = HourlyRate * HoursWorked;
        Console.WriteLine("Contract Employee Salary: " + totalSalary);
    }
}

class Program
{
    static void Main()
    {
        // Runtime polymorphism using interface reference
        IEmployee emp;

        emp = new PermanentEmployee(20000);
        emp.CalculateSalary();

        emp = new ContractEmployee(500, 40);
        emp.CalculateSalary();
    }
}
