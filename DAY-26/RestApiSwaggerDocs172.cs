// REST API with Swagger / OpenAPI Documentation


// Simulated demo — shows what Swagger generates as documentation output
using System;
using System.Collections.Generic;
using System.Text.Json;

record Product(int Id, string Name, decimal Price);

class RestApiSwaggerDocs
{
    static List<Product> products = new()
    {
        new(1, "Laptop",  75000m),
        new(2, "Mouse",   1500m),
        new(3, "Keyboard",2500m),
    };

    static void Main()
    {
        Console.WriteLine("=== REST API with Swagger Documentation (simulated) ===\n");
        Console.WriteLine("Swagger UI URL (when running): https://localhost:5001/swagger\n");

        PrintSwaggerSpec();
        Console.WriteLine("\n--- Live Endpoint Simulation ---\n");

        SimulateGet("/products");
        SimulateGetById(2);
        SimulateGetById(99);
        SimulatePost(new Product(0, "Tablet", 30000m));
        SimulateDelete(2);
        SimulateGet("/products");
    }

    static void PrintSwaggerSpec()
    {
        Console.WriteLine("[Swagger OpenAPI Info]");
        Console.WriteLine("  Title  : Product API");
        Console.WriteLine("  Version: v1");
        Console.WriteLine("  Endpoints:");
        Console.WriteLine("    GET    /products          -> 200 List<Product>");
        Console.WriteLine("    GET    /products/{id}     -> 200 Product | 404");
        Console.WriteLine("    POST   /products          -> 201 Product | 400");
        Console.WriteLine("    DELETE /products/{id}     -> 204 | 404");
    }

    static void SimulateGet(string path)
    {
        Console.WriteLine($"GET {path}");
        Console.WriteLine("  200 OK => " + JsonSerializer.Serialize(products));
        Console.WriteLine();
    }

    static void SimulateGetById(int id)
    {
        var p = products.Find(x => x.Id == id);
        Console.WriteLine($"GET /products/{id}");
        Console.WriteLine(p is null ? "  404 Not Found" : "  200 OK => " + JsonSerializer.Serialize(p));
        Console.WriteLine();
    }

    static void SimulatePost(Product p)
    {
        p = p with { Id = products.Count + 1 };
        products.Add(p);
        Console.WriteLine("POST /products");
        Console.WriteLine("  201 Created => " + JsonSerializer.Serialize(p));
        Console.WriteLine();
    }

    static void SimulateDelete(int id)
    {
        var p = products.Find(x => x.Id == id);
        Console.WriteLine($"DELETE /products/{id}");
        if (p is null) { Console.WriteLine("  404 Not Found\n"); return; }
        products.Remove(p);
        Console.WriteLine("  204 No Content\n");
    }
}
