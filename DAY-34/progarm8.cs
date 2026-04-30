// Program to use ILookup<TKey, TElement> for one-to-many data grouping
using System;
using System.Collections.Generic;
using System.Linq;

class Employee
{
    public string Name { get; set; }
    public string Department { get; set; }
}

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "Alice", Department = "IT" },
            new Employee { Name = "Bob", Department = "HR" },
            new Employee { Name = "Charlie", Department = "IT" },
            new Employee { Name = "David", Department = "HR" },
            new Employee { Name = "Eve", Department = "Finance" }
        };

        ILookup<string, Employee> lookup = employees.ToLookup(e => e.Department);

        Console.WriteLine("Employees grouped by department using ILookup:");
        foreach (var group in lookup)
        {
            Console.WriteLine($"Department: {group.Key}");
            foreach (var emp in group)
            {
                Console.WriteLine($" - {emp.Name}");
            }
        }
    }
}