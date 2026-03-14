// REST API with Pagination and Filtering


// Simulated demo — full pagination + filtering logic (no web server)
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
}

class PagedResult<T>
{
    public int      Page       { get; set; }
    public int      PageSize   { get; set; }
    public int      TotalCount { get; set; }
    public int      TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool     HasPrev    => Page > 1;
    public bool     HasNext    => Page < TotalPages;
    public List<T>  Data       { get; set; } = new();
}

class RestApiPaginationFiltering
{
    static readonly List<Product> AllProducts = Enumerable.Range(1, 20).Select(i => new Product
    {
        Id       = i,
        Name     = $"Product {i}",
        Category = i % 3 == 0 ? "Electronics" : i % 3 == 1 ? "Clothing" : "Stationery",
        Price    = (i * 150m) % 5000 + 100
    }).ToList();

    static PagedResult<Product> GetProducts(
        string? name     = null,
        string? category = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        string  sortBy   = "Id",
        bool    desc     = false,
        int     page     = 1,
        int     pageSize = 5)
    {
        IEnumerable<Product> q = AllProducts;

        // Filtering
        if (!string.IsNullOrEmpty(name))
            q = q.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrEmpty(category))
            q = q.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        if (minPrice.HasValue) q = q.Where(p => p.Price >= minPrice.Value);
        if (maxPrice.HasValue) q = q.Where(p => p.Price <= maxPrice.Value);

        // Sorting
        q = sortBy.ToLower() switch
        {
            "name"  => desc ? q.OrderByDescending(p => p.Name)  : q.OrderBy(p => p.Name),
            "price" => desc ? q.OrderByDescending(p => p.Price) : q.OrderBy(p => p.Price),
            _       => desc ? q.OrderByDescending(p => p.Id)    : q.OrderBy(p => p.Id)
        };

        // Pagination
        var list  = q.ToList();
        int total = list.Count;
        page     = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 50);

        return new PagedResult<Product>
        {
            Page       = page,
            PageSize   = pageSize,
            TotalCount = total,
            Data       = list.Skip((page - 1) * pageSize).Take(pageSize).ToList()
        };
    }

    static void Print(string label, PagedResult<Product> r)
    {
        Console.WriteLine($"{label}");
        Console.WriteLine($"  Page {r.Page}/{r.TotalPages}  |  Total: {r.TotalCount}  |  HasPrev: {r.HasPrev}  HasNext: {r.HasNext}");
        foreach (var p in r.Data)
            Console.WriteLine($"    [{p.Id,2}] {p.Name,-12} {p.Category,-12} ₹{p.Price}");
        Console.WriteLine();
    }

    static void Main()
    {
        Console.WriteLine("=== REST API with Pagination and Filtering ===\n");

        Print("GET /products?page=1&pageSize=5",
              GetProducts(page: 1, pageSize: 5));

        Print("GET /products?page=2&pageSize=5",
              GetProducts(page: 2, pageSize: 5));

        Print("GET /products?category=Electronics&pageSize=5",
              GetProducts(category: "Electronics", pageSize: 5));

        Print("GET /products?minPrice=500&maxPrice=2000&sortBy=price&desc=true",
              GetProducts(minPrice: 500, maxPrice: 2000, sortBy: "price", desc: true, pageSize: 5));
    }
}
