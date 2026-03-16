// Clean Architecture Implementation

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

// ── Domain Layer ───────────────────────────────────────────────────────────
class Product
{
    public int     Id    { get; private set; }
    public string  Name  { get; private set; }
    public decimal Price { get; private set; }
    public int     Stock { get; private set; }

    public Product(int id, string name, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required.");
        if (price < 0) throw new ArgumentException("Price cannot be negative.");
        Id = id; Name = name; Price = price; Stock = stock;
    }

    public void UpdatePrice(decimal price) { if (price < 0) throw new ArgumentException("Invalid price."); Price = price; }
    public void ReduceStock(int qty)       { if (qty > Stock) throw new InvalidOperationException("Insufficient stock."); Stock -= qty; }
}

interface IProductRepository
{
    Product?      GetById(int id);
    List<Product> GetAll();
    void          Add(Product p);
    void          Update(Product p);
    void          Delete(int id);
}

// ── Application Layer ──────────────────────────────────────────────────────
record CreateProductCommand(string Name, decimal Price, int Stock);
record UpdatePriceCommand(int Id, decimal NewPrice);
record PlaceOrderCommand(int ProductId, int Quantity);
record ProductDto(int Id, string Name, decimal Price, int Stock);

class ProductService
{
    readonly IProductRepository _repo;
    int _nextId = 1;

    public ProductService(IProductRepository repo) { _repo = repo; }

    public ProductDto Create(CreateProductCommand cmd)
    {
        var p = new Product(_nextId++, cmd.Name, cmd.Price, cmd.Stock);
        _repo.Add(p);
        return Map(p);
    }

    public ProductDto? GetById(int id) { var p = _repo.GetById(id); return p is null ? null : Map(p); }
    public List<ProductDto> GetAll()   => _repo.GetAll().Select(Map).ToList();

    public ProductDto UpdatePrice(UpdatePriceCommand cmd)
    {
        var p = _repo.GetById(cmd.Id) ?? throw new KeyNotFoundException($"Product {cmd.Id} not found.");
        p.UpdatePrice(cmd.NewPrice);
        _repo.Update(p);
        return Map(p);
    }

    public string PlaceOrder(PlaceOrderCommand cmd)
    {
        var p = _repo.GetById(cmd.ProductId) ?? throw new KeyNotFoundException($"Product {cmd.ProductId} not found.");
        p.ReduceStock(cmd.Quantity);
        _repo.Update(p);
        return $"Order: {cmd.Quantity}x {p.Name} | Total: Rs.{p.Price * cmd.Quantity} | Stock left: {p.Stock}";
    }

    static ProductDto Map(Product p) => new(p.Id, p.Name, p.Price, p.Stock);
}

// ── Infrastructure Layer ───────────────────────────────────────────────────
class InMemoryProductRepository : IProductRepository
{
    readonly List<Product> _store = new();
    public Product?      GetById(int id) => _store.FirstOrDefault(p => p.Id == id);
    public List<Product> GetAll()        => _store.ToList();
    public void          Add(Product p)  => _store.Add(p);
    public void          Update(Product p) { }
    public void          Delete(int id)  => _store.RemoveAll(p => p.Id == id);
}

// ── Presentation Layer ─────────────────────────────────────────────────────
class CleanArchitectureImplementation
{
    static void Main()
    {
        Console.WriteLine("=== Clean Architecture Implementation ===\n");

        Console.WriteLine(@"  Layers (dependency rule: outer → inward only):
  ┌─────────────────────────────────────┐
  │   Presentation  (API / Controllers) │
  ├─────────────────────────────────────┤
  │   Application   (Use Cases, DTOs)   │
  ├─────────────────────────────────────┤
  │   Domain        (Entities, Interfaces) │
  ├─────────────────────────────────────┤
  │   Infrastructure (EF Core, Redis…)  │
  └─────────────────────────────────────┘
");

        var service = new ProductService(new InMemoryProductRepository());

        service.Create(new("Laptop",   75000, 10));
        service.Create(new("Mouse",    1500,  50));
        service.Create(new("Keyboard", 2500,  30));

        Console.WriteLine("─── All Products ─────────────────────────────────");
        foreach (var p in service.GetAll())
            Console.WriteLine($"  [{p.Id}] {p.Name,-12} Rs.{p.Price,-8} Stock:{p.Stock}");

        Console.WriteLine("\n─── Update Laptop price to 80000 ─────────────────");
        var updated = service.UpdatePrice(new(1, 80000));
        Console.WriteLine("  " + JsonSerializer.Serialize(updated));

        Console.WriteLine("\n─── Place Order: 3x Mouse ────────────────────────");
        Console.WriteLine("  " + service.PlaceOrder(new(2, 3)));

        Console.WriteLine("\n─── Place Order: 60x Mouse (exceeds stock) ───────");
        try { service.PlaceOrder(new(2, 60)); }
        catch (Exception ex) { Console.WriteLine("  Error: " + ex.Message); }

        Console.WriteLine("\n─── Final State ──────────────────────────────────");
        foreach (var p in service.GetAll())
            Console.WriteLine($"  [{p.Id}] {p.Name,-12} Rs.{p.Price,-8} Stock:{p.Stock}");
    }
}
