// CQRS Pattern Implementation

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

// ── Domain ─────────────────────────────────────────────────────────────────
class OrderItem { public string Product { get; set; } = ""; public int Qty { get; set; } public decimal Price { get; set; } }
class Order     { public int Id { get; set; } public string Customer { get; set; } = ""; public List<OrderItem> Items { get; set; } = new(); public decimal Total => Items.Sum(i => i.Price * i.Qty); public string Status { get; set; } = "Pending"; public DateTime CreatedAt { get; set; } }

// ── Commands (write side) ──────────────────────────────────────────────────
record CreateOrderCommand(string Customer, List<OrderItem> Items);
record UpdateOrderStatusCommand(int OrderId, string NewStatus);
record DeleteOrderCommand(int OrderId);

interface ICommandHandler<TCommand, TResult> { TResult Handle(TCommand command); }

class CreateOrderHandler : ICommandHandler<CreateOrderCommand, Order>
{
    readonly List<Order> _store;
    int _nextId;
    public CreateOrderHandler(List<Order> store, ref int nextId) { _store = store; _nextId = nextId; }

    public Order Handle(CreateOrderCommand cmd)
    {
        var order = new Order { Id = _nextId++, Customer = cmd.Customer, Items = cmd.Items, CreatedAt = DateTime.UtcNow };
        _store.Add(order);
        return order;
    }
}

class UpdateOrderStatusHandler : ICommandHandler<UpdateOrderStatusCommand, bool>
{
    readonly List<Order> _store;
    public UpdateOrderStatusHandler(List<Order> store) { _store = store; }

    public bool Handle(UpdateOrderStatusCommand cmd)
    {
        var order = _store.FirstOrDefault(o => o.Id == cmd.OrderId);
        if (order is null) return false;
        order.Status = cmd.NewStatus;
        return true;
    }
}

class DeleteOrderHandler : ICommandHandler<DeleteOrderCommand, bool>
{
    readonly List<Order> _store;
    public DeleteOrderHandler(List<Order> store) { _store = store; }
    public bool Handle(DeleteOrderCommand cmd) => _store.RemoveAll(o => o.Id == cmd.OrderId) > 0;
}

// ── Queries (read side) ────────────────────────────────────────────────────
record GetOrderByIdQuery(int OrderId);
record GetOrdersByCustomerQuery(string Customer);
record GetOrdersSummaryQuery(string? Status = null);

record OrderSummaryDto(int Id, string Customer, decimal Total, string Status, DateTime CreatedAt);

interface IQueryHandler<TQuery, TResult> { TResult Handle(TQuery query); }

class GetOrderByIdHandler : IQueryHandler<GetOrderByIdQuery, Order?>
{
    readonly List<Order> _store;
    public GetOrderByIdHandler(List<Order> store) { _store = store; }
    public Order? Handle(GetOrderByIdQuery q) => _store.FirstOrDefault(o => o.Id == q.OrderId);
}

class GetOrdersByCustomerHandler : IQueryHandler<GetOrdersByCustomerQuery, List<Order>>
{
    readonly List<Order> _store;
    public GetOrdersByCustomerHandler(List<Order> store) { _store = store; }
    public List<Order> Handle(GetOrdersByCustomerQuery q) =>
        _store.Where(o => o.Customer.Equals(q.Customer, StringComparison.OrdinalIgnoreCase)).ToList();
}

class GetOrdersSummaryHandler : IQueryHandler<GetOrdersSummaryQuery, List<OrderSummaryDto>>
{
    readonly List<Order> _store;
    public GetOrdersSummaryHandler(List<Order> store) { _store = store; }
    public List<OrderSummaryDto> Handle(GetOrdersSummaryQuery q)
    {
        var orders = string.IsNullOrEmpty(q.Status) ? _store : _store.Where(o => o.Status == q.Status);
        return orders.Select(o => new OrderSummaryDto(o.Id, o.Customer, o.Total, o.Status, o.CreatedAt)).ToList();
    }
}

// ── Mediator (dispatcher) ──────────────────────────────────────────────────
class Mediator
{
    readonly Dictionary<Type, object> _handlers = new();
    public void Register<TCommand, TResult>(ICommandHandler<TCommand, TResult> h) => _handlers[typeof(TCommand)] = h;
    public void Register<TQuery, TResult>(IQueryHandler<TQuery, TResult> h) => _handlers[typeof(TQuery)] = h;
    public TResult Send<TCommand, TResult>(TCommand cmd) where TCommand : notnull =>
        ((ICommandHandler<TCommand, TResult>)_handlers[typeof(TCommand)]).Handle(cmd);
    public TResult Query<TQuery, TResult>(TQuery q) where TQuery : notnull =>
        ((IQueryHandler<TQuery, TResult>)_handlers[typeof(TQuery)]).Handle(q);
}

class CQRSPatternImplementation
{
    static void Main()
    {
        Console.WriteLine("=== CQRS Pattern Implementation ===\n");

        Console.WriteLine("  CQRS = Command Query Responsibility Segregation");
        Console.WriteLine("  Commands: change state (Create, Update, Delete)");
        Console.WriteLine("  Queries : read state   (Get, List, Search)\n");

        var store  = new List<Order>();
        int nextId = 1;
        var med    = new Mediator();

        med.Register<CreateOrderCommand, Order>(new CreateOrderHandler(store, ref nextId));
        med.Register<UpdateOrderStatusCommand, bool>(new UpdateOrderStatusHandler(store));
        med.Register<DeleteOrderCommand, bool>(new DeleteOrderHandler(store));
        med.Register<GetOrderByIdQuery, Order?>(new GetOrderByIdHandler(store));
        med.Register<GetOrdersByCustomerQuery, List<Order>>(new GetOrdersByCustomerHandler(store));
        med.Register<GetOrdersSummaryQuery, List<OrderSummaryDto>>(new GetOrdersSummaryHandler(store));

        Console.WriteLine("─── Commands ─────────────────────────────────────");
        var o1 = med.Send<CreateOrderCommand, Order>(new("Alice", new() { new() { Product="Laptop",   Qty=1, Price=75000 } }));
        var o2 = med.Send<CreateOrderCommand, Order>(new("Bob",   new() { new() { Product="Mouse",    Qty=2, Price=1500  }, new() { Product="Keyboard", Qty=1, Price=2500 } }));
        var o3 = med.Send<CreateOrderCommand, Order>(new("Alice", new() { new() { Product="Monitor",  Qty=1, Price=18000 } }));
        Console.WriteLine($"  Created Order #{o1.Id} for {o1.Customer} | Total: Rs.{o1.Total}");
        Console.WriteLine($"  Created Order #{o2.Id} for {o2.Customer} | Total: Rs.{o2.Total}");
        Console.WriteLine($"  Created Order #{o3.Id} for {o3.Customer} | Total: Rs.{o3.Total}");

        bool updated = med.Send<UpdateOrderStatusCommand, bool>(new(o1.Id, "Shipped"));
        Console.WriteLine($"  Update #{o1.Id} status → Shipped: {updated}");

        bool deleted = med.Send<DeleteOrderCommand, bool>(new(o3.Id));
        Console.WriteLine($"  Delete #{o3.Id}: {deleted}");

        Console.WriteLine("\n─── Queries ──────────────────────────────────────");
        var found = med.Query<GetOrderByIdQuery, Order?>(new(o2.Id));
        Console.WriteLine($"  GetById #{o2.Id}: {found?.Customer}, Rs.{found?.Total}");

        var aliceOrders = med.Query<GetOrdersByCustomerQuery, List<Order>>(new("Alice"));
        Console.WriteLine($"  Alice's orders: {aliceOrders.Count} found");
        foreach (var o in aliceOrders) Console.WriteLine($"    #{o.Id} - {o.Status} - Rs.{o.Total}");

        Console.WriteLine("\n  Summary (all):");
        var summary = med.Query<GetOrdersSummaryQuery, List<OrderSummaryDto>>(new());
        foreach (var s in summary) Console.WriteLine($"    #{s.Id} {s.Customer,-8} Rs.{s.Total,-8} [{s.Status}]");
    }
}
