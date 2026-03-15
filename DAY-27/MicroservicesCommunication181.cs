// Microservices Communication (Basic)

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

record Product(int Id, string Name, decimal Price);
record Order(int OrderId, int ProductId, int Quantity, decimal TotalPrice, string Status);

class ProductService
{
    static readonly List<Product> _products = new()
    {
        new(1, "Laptop",   75000m),
        new(2, "Mouse",    1500m),
        new(3, "Keyboard", 2500m),
    };

    public Product? GetById(int id) => _products.Find(p => p.Id == id);
    public List<Product> GetAll()   => _products;
}

class OrderService
{
    readonly ProductService _productService;
    readonly List<Order> _orders = new();
    int _nextId = 1;

    public OrderService(ProductService productService) { _productService = productService; }

    public (Order? order, string error) PlaceOrder(int productId, int quantity)
    {
        var product = _productService.GetById(productId);
        if (product is null) return (null, $"Product {productId} not found.");

        var order = new Order(_nextId++, productId, quantity, product.Price * quantity, "Confirmed");
        _orders.Add(order);
        return (order, "");
    }

    public List<Order> GetAll() => _orders;
}

class NotificationService
{
    public void Notify(string channel, string message) =>
        Console.WriteLine($"  [Notification:{channel}] {message}");
}

class MicroservicesCommunicationDemo
{
    static void Main()
    {
        var productSvc      = new ProductService();
        var orderSvc        = new OrderService(productSvc);
        var notificationSvc = new NotificationService();

        Console.WriteLine("=== Microservices Communication (Basic) ===\n");

        Console.WriteLine("[API Gateway] GET /products");
        foreach (var p in productSvc.GetAll())
            Console.WriteLine($"  {p.Id}. {p.Name} - Rs.{p.Price}");

        Console.WriteLine("\n[API Gateway] POST /orders  {productId:1, quantity:2}");
        var (order, err) = orderSvc.PlaceOrder(1, 2);
        if (order is not null)
        {
            Console.WriteLine($"  Order #{order.OrderId}: {order.Status} | Total: Rs.{order.TotalPrice}");
            notificationSvc.Notify("Email", $"Order #{order.OrderId} placed for Product {order.ProductId}.");
            notificationSvc.Notify("SMS",   $"Your order #{order.OrderId} is confirmed.");
        }
        else Console.WriteLine($"  Error: {err}");

        Console.WriteLine("\n[API Gateway] POST /orders  {productId:99, quantity:1}");
        var (order2, err2) = orderSvc.PlaceOrder(99, 1);
        Console.WriteLine(order2 is null ? $"  404: {err2}" : $"  Order #{order2.OrderId} placed.");

        Console.WriteLine("\n[API Gateway] GET /orders");
        foreach (var o in orderSvc.GetAll())
            Console.WriteLine($"  Order #{o.OrderId} | Product: {o.ProductId} | Qty: {o.Quantity} | Status: {o.Status}");
    }
}
