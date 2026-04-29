// Program to design a Shopping Cart model (Product, Cart, Customer)
using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

class Cart
{
    private List<Product> products = new List<Product>();

    public void AddProduct(Product product)
    {
        products.Add(product);
        Console.WriteLine($"{product.Name} added to cart.");
    }

    public void RemoveProduct(Product product)
    {
        products.Remove(product);
        Console.WriteLine($"{product.Name} removed from cart.");
    }

    public decimal CalculateTotal()
    {
        decimal total = 0;
        foreach (var product in products)
            total += product.Price;
        return total;
    }

    public void ShowCart()
    {
        Console.WriteLine("Cart Contents:");
        foreach (var product in products)
            Console.WriteLine($"- {product.Name}: ${product.Price}");
        Console.WriteLine("Total: $" + CalculateTotal());
    }
}

class Customer
{
    public string Name { get; set; }
    public Cart Cart { get; set; }

    public Customer(string name)
    {
        Name = name;
        Cart = new Cart();
    }

    public void Checkout()
    {
        Console.WriteLine($"{Name} is checking out...");
        Cart.ShowCart();
    }
}

class Program
{
    static void Main()
    {
        Customer customer = new Customer("Alice");

        Product p1 = new Product("Laptop", 1200);
        Product p2 = new Product("Mouse", 25);
        Product p3 = new Product("Keyboard", 50);

        customer.Cart.AddProduct(p1);
        customer.Cart.AddProduct(p2);
        customer.Cart.AddProduct(p3);

        customer.Checkout();
    }
}
