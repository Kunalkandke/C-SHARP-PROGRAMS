// Program to group employees by department and calculate Average Salary per group
using System;
using System.Collections.Generic;
using System.Linq;

class Employee
{
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
}

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "Alice", Department = "IT", Salary = 60000 },
            new Employee { Name = "Bob", Department = "HR", Salary = 50000 },
            new Employee { Name = "Charlie", Department = "IT", Salary = 70000 },
            new Employee { Name = "David", Department = "HR", Salary = 55000 },
            new Employee { Name = "Eve", Department = "Finance", Salary = 65000 }
        };

        var avgSalaryByDept = employees
            .GroupBy(emp => emp.Department)
            .Select(g => new 
            { 
                Department = g.Key, 
                AverageSalary = g.Average(emp => emp.Salary) 
            });

        Console.WriteLine("Average Salary by Department:");
        foreach (var dept in avgSalaryByDept)
        {
            Console.WriteLine($"{dept.Department}: {dept.AverageSalary}");
        }
    }
}