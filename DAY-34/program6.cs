// Program to build a dynamic LINQ query using Expression Trees
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

class Program
{
    static void Main()
    {
        List<Product> products = new List<Product>
        {
            new Product { Name = "Laptop", Price = 1200 },
            new Product { Name = "Mouse", Price = 25 },
            new Product { Name = "Keyboard", Price = 50 },
            new Product { Name = "Monitor", Price = 300 }
        };

        // Build dynamic query: products where Price > 100
        var parameter = Expression.Parameter(typeof(Product), "p");
        var property = Expression.Property(parameter, "Price");
        var constant = Expression.Constant(100m);
        var comparison = Expression.GreaterThan(property, constant);
        var lambda = Expression.Lambda<Func<Product, bool>>(comparison, parameter);

        var filteredProducts = products.AsQueryable().Where(lambda);

        Console.WriteLine("Products with Price > 100:");
        foreach (var p in filteredProducts)
        {
            Console.WriteLine($"{p.Name} - {p.Price}");
        }
    }
}