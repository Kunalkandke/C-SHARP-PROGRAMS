// REST API using ASP.NET Core (Basic — Minimal API)


// Simulated demo — runs in-process, shows route dispatch logic
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

record Product(int Id, string Name, decimal Price);

class AspNetCoreRestApiBasic
{
    static List<Product> products = new()
    {
        new(1, "Laptop",   75000),
        new(2, "Mouse",    1500),
        new(3, "Keyboard", 2500),
    };

    static void Main()
    {
        Console.WriteLine("=== REST API using ASP.NET Core — Minimal API (simulated) ===\n");

        SimulateGet();
        SimulateGetById(2);
        SimulatePost(new Product(0, "Tablet", 30000));
        SimulatePut(1, new Product(1, "Gaming Laptop", 90000));
        SimulateDelete(2);
        SimulateGet(); // show final state
    }

    static void SimulateGet()
    {
        Console.WriteLine("GET /products");
        Console.WriteLine("  " + JsonSerializer.Serialize(products));
        Console.WriteLine();
    }

    static void SimulateGetById(int id)
    {
        var p = products.FirstOrDefault(x => x.Id == id);
        Console.WriteLine($"GET /products/{id}");
        Console.WriteLine(p is null ? "  404 Not Found" : "  " + JsonSerializer.Serialize(p));
        Console.WriteLine();
    }

    static void SimulatePost(Product newProduct)
    {
        newProduct = newProduct with { Id = products.Max(p => p.Id) + 1 };
        products.Add(newProduct);
        Console.WriteLine("POST /products");
        Console.WriteLine($"  201 Created => {JsonSerializer.Serialize(newProduct)}");
        Console.WriteLine();
    }

    static void SimulatePut(int id, Product updated)
    {
        int idx = products.FindIndex(p => p.Id == id);
        Console.WriteLine($"PUT /products/{id}");
        if (idx == -1) { Console.WriteLine("  404 Not Found"); return; }
        products[idx] = updated with { Id = id };
        Console.WriteLine($"  200 OK => {JsonSerializer.Serialize(products[idx])}");
        Console.WriteLine();
    }

    static void SimulateDelete(int id)
    {
        var p = products.FirstOrDefault(x => x.Id == id);
        Console.WriteLine($"DELETE /products/{id}");
        if (p is null) { Console.WriteLine("  404 Not Found\n"); return; }
        products.Remove(p);
        Console.WriteLine("  204 No Content\n");
    }
}
