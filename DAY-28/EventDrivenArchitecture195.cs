// Event-driven Architecture (Basic)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// ── Events (plain data, no logic) ─────────────────────────────────────────
abstract class DomainEvent { public DateTime OccurredAt { get; } = DateTime.UtcNow; }

class OrderPlacedEvent     : DomainEvent { public int OrderId { get; init; } public string Customer { get; init; } = ""; public decimal Total { get; init; } }
class OrderShippedEvent    : DomainEvent { public int OrderId { get; init; } public string TrackingNo { get; init; } = ""; }
class OrderCancelledEvent  : DomainEvent { public int OrderId { get; init; } public string Reason { get; init; } = ""; }
class LowStockEvent        : DomainEvent { public string Product { get; init; } = ""; public int RemainingStock { get; init; } }

// ── Event Bus (publish / subscribe) ───────────────────────────────────────
class EventBus
{
    readonly Dictionary<Type, List<Func<DomainEvent, Task>>> _handlers = new();

    public void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : DomainEvent
    {
        if (!_handlers.ContainsKey(typeof(TEvent))) _handlers[typeof(TEvent)] = new();
        _handlers[typeof(TEvent)].Add(e => handler((TEvent)e));
    }

    public async Task Publish<TEvent>(TEvent evt) where TEvent : DomainEvent
    {
        Console.WriteLine($"\n  [EventBus] Published: {typeof(TEvent).Name} @ {evt.OccurredAt:HH:mm:ss}");
        if (!_handlers.TryGetValue(typeof(TEvent), out var handlers)) return;
        foreach (var h in handlers) await h(evt);
    }
}

// ── Event Handlers (subscribers) ──────────────────────────────────────────
class EmailNotificationHandler
{
    public Task OnOrderPlaced(OrderPlacedEvent e)
    {
        Console.WriteLine($"    [EmailService]   Sending confirmation to {e.Customer} for Order #{e.OrderId} (Rs.{e.Total})");
        return Task.CompletedTask;
    }

    public Task OnOrderShipped(OrderShippedEvent e)
    {
        Console.WriteLine($"    [EmailService]   Shipping notification for Order #{e.OrderId}, tracking: {e.TrackingNo}");
        return Task.CompletedTask;
    }

    public Task OnOrderCancelled(OrderCancelledEvent e)
    {
        Console.WriteLine($"    [EmailService]   Cancellation notice for Order #{e.OrderId}. Reason: {e.Reason}");
        return Task.CompletedTask;
    }
}

class InventoryHandler
{
    readonly Dictionary<string, int> _stock = new()
    {
        ["Laptop"] = 10, ["Mouse"] = 50, ["Keyboard"] = 30
    };

    readonly EventBus _bus;
    public InventoryHandler(EventBus bus) { _bus = bus; }

    public async Task OnOrderPlaced(OrderPlacedEvent e)
    {
        Console.WriteLine($"    [Inventory]      Reserving stock for Order #{e.OrderId}");
        string product = "Laptop";
        _stock[product]--;
        Console.WriteLine($"    [Inventory]      {product} stock: {_stock[product]}");
        if (_stock[product] < 5)
            await _bus.Publish(new LowStockEvent { Product = product, RemainingStock = _stock[product] });
    }
}

class AuditLogHandler
{
    readonly List<string> _log = new();

    public Task OnAny(DomainEvent e)
    {
        string entry = $"{e.OccurredAt:HH:mm:ss} | {e.GetType().Name} | {JsonSerializer.Serialize(e)}";
        _log.Add(entry);
        Console.WriteLine($"    [AuditLog]       Logged: {e.GetType().Name}");
        return Task.CompletedTask;
    }

    public void PrintLog()
    {
        Console.WriteLine("\n─── Audit Log ────────────────────────────────────");
        foreach (var l in _log) Console.WriteLine($"  {l}");
    }
}

class PurchaseOrderHandler
{
    public Task OnLowStock(LowStockEvent e)
    {
        Console.WriteLine($"    [Procurement]    Auto-creating PO for {e.Product} (only {e.RemainingStock} left)");
        return Task.CompletedTask;
    }
}

// ── Domain Model ───────────────────────────────────────────────────────────
class OrderAggregate
{
    static int _nextId = 1;
    public int Id { get; } = _nextId++;
    public string Status { get; private set; } = "Pending";

    readonly EventBus _bus;
    public OrderAggregate(EventBus bus) { _bus = bus; }

    public async Task Place(string customer, decimal total)
    {
        Status = "Placed";
        await _bus.Publish(new OrderPlacedEvent { OrderId = Id, Customer = customer, Total = total });
    }

    public async Task Ship(string trackingNo)
    {
        Status = "Shipped";
        await _bus.Publish(new OrderShippedEvent { OrderId = Id, TrackingNo = trackingNo });
    }

    public async Task Cancel(string reason)
    {
        Status = "Cancelled";
        await _bus.Publish(new OrderCancelledEvent { OrderId = Id, Reason = reason });
    }
}

class EventDrivenArchitecture
{
    static async Task Main()
    {
        Console.WriteLine("=== Event-driven Architecture (Basic) ===\n");

        var bus       = new EventBus();
        var email     = new EmailNotificationHandler();
        var inventory = new InventoryHandler(bus);
        var audit     = new AuditLogHandler();
        var po        = new PurchaseOrderHandler();

        bus.Subscribe<OrderPlacedEvent>(email.OnOrderPlaced);
        bus.Subscribe<OrderPlacedEvent>(inventory.OnOrderPlaced);
        bus.Subscribe<OrderPlacedEvent>(e => audit.OnAny(e));
        bus.Subscribe<OrderShippedEvent>(email.OnOrderShipped);
        bus.Subscribe<OrderShippedEvent>(e => audit.OnAny(e));
        bus.Subscribe<OrderCancelledEvent>(email.OnOrderCancelled);
        bus.Subscribe<OrderCancelledEvent>(e => audit.OnAny(e));
        bus.Subscribe<LowStockEvent>(po.OnLowStock);
        bus.Subscribe<LowStockEvent>(e => audit.OnAny(e));

        Console.WriteLine("─── Order Lifecycle ──────────────────────────────");

        var order1 = new OrderAggregate(bus);
        await order1.Place("Alice", 75000);
        await order1.Ship("TRK-001-XYZ");

        var order2 = new OrderAggregate(bus);
        await order2.Place("Bob", 5500);
        await order2.Cancel("Customer requested cancellation");

        audit.PrintLog();
    }
}
