// REST API with Entity Framework Core

// Simulated demo — in-memory EF Core-style CRUD (no SQL Server required)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

class Product
{
    public int     Id       { get; set; }
    public string  Name     { get; set; } = "";
    public string  Category { get; set; } = "";
    public decimal Price    { get; set; }
    public int     Stock    { get; set; }
}

// Minimal in-memory DbContext substitute
class AppDbContext
{
    public List<Product> Products { get; } = new()
    {
        new() { Id=1, Name="Laptop",   Category="Electronics", Price=75000, Stock=10  },
        new() { Id=2, Name="Mouse",    Category="Electronics", Price=1500,  Stock=50  },
        new() { Id=3, Name="Notebook", Category="Stationery",  Price=50,    Stock=200 },
    };
    private int _nextId = 4;

    public Product? Find(int id) => Products.FirstOrDefault(p => p.Id == id);

    public Product Add(Product p)   { p.Id = _nextId++; Products.Add(p); return p; }
    public void Remove(Product p)   { Products.Remove(p); }
    public void SaveChanges()       { /* committed */ }
}

class RestApiEntityFrameworkCore
{
    static readonly AppDbContext db = new();

    static void Main()
    {
        Console.WriteLine("=== REST API with Entity Framework Core (simulated) ===\n");

        MapGet();
        MapGetById(2);
        MapPost(new Product { Name="Tablet", Category="Electronics", Price=30000, Stock=15 });
        MapPut(1, new Product { Name="Gaming Laptop", Category="Electronics", Price=90000, Stock=5 });
        MapDelete(2);
        MapGet();
    }

    static void MapGet()
    {
        Console.WriteLine("GET /products");
        Console.WriteLine("  200 OK => " + JsonSerializer.Serialize(db.Products));
        Console.WriteLine();
    }

    static void MapGetById(int id)
    {
        var p = db.Find(id);
        Console.WriteLine($"GET /products/{id}");
        Console.WriteLine(p is null ? "  404 Not Found" : "  200 OK => " + JsonSerializer.Serialize(p));
        Console.WriteLine();
    }

    static void MapPost(Product p)
    {
        var created = db.Add(p);
        db.SaveChanges();
        Console.WriteLine("POST /products");
        Console.WriteLine("  201 Created => " + JsonSerializer.Serialize(created));
        Console.WriteLine();
    }

    static void MapPut(int id, Product updated)
    {
        var existing = db.Find(id);
        Console.WriteLine($"PUT /products/{id}");
        if (existing is null) { Console.WriteLine("  404 Not Found\n"); return; }
        existing.Name = updated.Name; existing.Category = updated.Category;
        existing.Price = updated.Price; existing.Stock = updated.Stock;
        db.SaveChanges();
        Console.WriteLine("  200 OK => " + JsonSerializer.Serialize(existing));
        Console.WriteLine();
    }

    static void MapDelete(int id)
    {
        var p = db.Find(id);
        Console.WriteLine($"DELETE /products/{id}");
        if (p is null) { Console.WriteLine("  404 Not Found\n"); return; }
        db.Remove(p); db.SaveChanges();
        Console.WriteLine("  204 No Content\n");
    }
}
