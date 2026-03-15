// Azure SQL Integration with .NET

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

class Employee
{
    public int    Id         { get; set; }
    public string Name       { get; set; } = "";
    public string Department { get; set; } = "";
    public decimal Salary    { get; set; }
}

class AzureSqlIntegrationDemo
{
    static readonly List<Employee> _db = new()
    {
        new() { Id=1, Name="Alice",   Department="Engineering", Salary=90000 },
        new() { Id=2, Name="Bob",     Department="Marketing",   Salary=70000 },
        new() { Id=3, Name="Charlie", Department="Engineering", Salary=95000 },
        new() { Id=4, Name="Diana",   Department="HR",          Salary=65000 },
    };
    static int _nextId = 5;

    static void Main()
    {
        Console.WriteLine("=== Azure SQL Integration with .NET ===\n");

        PrintConnectionSetup();
        PrintEntityFrameworkSetup();
        RunCrudDemo();
    }

    static void PrintConnectionSetup()
    {
        Console.WriteLine("─── Azure SQL Connection (appsettings.json) ──────");
        Console.WriteLine(@"  {
    ""ConnectionStrings"": {
      ""AzureSQL"": ""Server=tcp:myserver.database.windows.net,1433;
                     Initial Catalog=MyDb;
                     User ID=adminuser;Password=Pass@123;
                     Encrypt=True;TrustServerCertificate=False;
                     Connection Timeout=30;""
    }
  }");
        Console.WriteLine();
    }

    static void PrintEntityFrameworkSetup()
    {
        Console.WriteLine("─── EF Core Setup ────────────────────────────────");
        Console.WriteLine(@"  // Program.cs
  builder.Services.AddDbContext<AppDbContext>(opt =>
      opt.UseSqlServer(builder.Configuration.GetConnectionString(""AzureSQL"")));

  // Migrations
  dotnet ef migrations add InitialCreate
  dotnet ef database update");
        Console.WriteLine();
    }

    static void RunCrudDemo()
    {
        Console.WriteLine("─── CRUD Operations (simulated) ──────────────────");

        Console.WriteLine("SELECT * FROM Employees:");
        foreach (var e in _db)
            Console.WriteLine($"  [{e.Id}] {e.Name,-10} {e.Department,-14} Rs.{e.Salary}");

        Console.WriteLine("\nINSERT Employee (Eve, Finance, 72000):");
        var newEmp = new Employee { Id=_nextId++, Name="Eve", Department="Finance", Salary=72000 };
        _db.Add(newEmp);
        Console.WriteLine($"  Inserted: {JsonSerializer.Serialize(newEmp)}");

        Console.WriteLine("\nSELECT WHERE Department = 'Engineering':");
        foreach (var e in _db.Where(e => e.Department == "Engineering"))
            Console.WriteLine($"  [{e.Id}] {e.Name} - Rs.{e.Salary}");

        Console.WriteLine("\nUPDATE Employee SET Salary=100000 WHERE Id=1:");
        var emp = _db.First(e => e.Id == 1);
        emp.Salary = 100000;
        Console.WriteLine($"  Updated: {JsonSerializer.Serialize(emp)}");

        Console.WriteLine("\nDELETE Employee WHERE Id=4:");
        _db.Remove(_db.First(e => e.Id == 4));
        Console.WriteLine("  Deleted. Remaining count: " + _db.Count);

        Console.WriteLine("\nAGGREGATE — AVG Salary by Department:");
        foreach (var g in _db.GroupBy(e => e.Department))
            Console.WriteLine($"  {g.Key,-14}: Avg Rs.{g.Average(e => e.Salary):F0}");
    }
}
