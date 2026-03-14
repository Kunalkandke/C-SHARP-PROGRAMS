// REST API with Caching using Redis


// Simulated demo — in-memory dictionary as Redis substitute
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

record Product(int Id, string Name, decimal Price);

class InMemoryCache
{
    record CacheEntry(string Value, DateTime ExpiresAt);
    readonly Dictionary<string, CacheEntry> _store = new();

    public void Set(string key, string value, TimeSpan ttl)
        => _store[key] = new CacheEntry(value, DateTime.UtcNow.Add(ttl));

    public string? Get(string key)
    {
        if (!_store.TryGetValue(key, out var entry)) return null;
        if (DateTime.UtcNow > entry.ExpiresAt) { _store.Remove(key); return null; }
        return entry.Value;
    }

    public void Remove(string key) => _store.Remove(key);
}

class RestApiRedisCaching
{
    static readonly List<Product> products = new()
    {
        new(1, "Laptop",  75000m),
        new(2, "Mouse",   1500m),
        new(3, "Keyboard",2500m),
    };
    static readonly InMemoryCache cache = new();
    const string CacheKey = "all_products";

    static void Main()
    {
        Console.WriteLine("=== REST API with Caching (Redis simulated) ===\n");

        // First call — cache miss
        Console.WriteLine("Request 1: GET /products");
        GetProducts();

        // Second call — cache hit
        Console.WriteLine("Request 2: GET /products");
        GetProducts();

        // Add product — invalidates cache
        Console.WriteLine("Request 3: POST /products  {name: Tablet, price: 30000}");
        AddProduct(new Product(0, "Tablet", 30000m));

        // Next call — cache miss again (was invalidated)
        Console.WriteLine("Request 4: GET /products  (cache invalidated)");
        GetProducts();
    }

    static void GetProducts()
    {
        string? cached = cache.Get(CacheKey);
        if (cached is not null)
        {
            Console.WriteLine("  [CACHE HIT]  200 OK => " + cached);
        }
        else
        {
            Console.WriteLine("  [CACHE MISS] Fetching from data source...");
            string json = JsonSerializer.Serialize(products);
            cache.Set(CacheKey, json, TimeSpan.FromMinutes(5));
            Console.WriteLine("  [CACHED]     200 OK => " + json);
        }
        Console.WriteLine();
    }

    static void AddProduct(Product p)
    {
        p = p with { Id = products.Max(x => x.Id) + 1 };
        products.Add(p);
        cache.Remove(CacheKey);   // invalidate
        Console.WriteLine("  201 Created => " + JsonSerializer.Serialize(p));
        Console.WriteLine("  [CACHE INVALIDATED]\n");
    }
}
