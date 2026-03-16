// Multi-tenant Application (Basic)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

record Tenant(string Id, string Name, string Domain, string DbSchema, bool IsActive);
record TenantUser(string Id, string TenantId, string Username, string Role);
record TenantProduct(int Id, string TenantId, string Name, decimal Price);

class TenantContext
{
    public Tenant? Current { get; private set; }
    public void Set(Tenant t) => Current = t;
    public void Clear()       => Current = null;
}

class TenantResolver
{
    readonly List<Tenant> _tenants;
    public TenantResolver(List<Tenant> tenants) { _tenants = tenants; }

    public Tenant? ResolveByDomain(string host) =>
        _tenants.FirstOrDefault(t => t.IsActive && t.Domain == host);

    public Tenant? ResolveByHeader(string tenantId) =>
        _tenants.FirstOrDefault(t => t.IsActive && t.Id == tenantId);
}

class TenantDataStore
{
    readonly Dictionary<string, List<TenantProduct>> _products = new();
    readonly Dictionary<string, List<TenantUser>>    _users    = new();
    int _nextProductId = 1;

    public void SeedTenant(string tenantId, TenantProduct[] products, TenantUser[] users)
    {
        _products[tenantId] = products.ToList();
        _users[tenantId]    = users.ToList();
        foreach (var p in _products[tenantId]) p = p with { Id = _nextProductId++ };
    }

    public List<TenantProduct> GetProducts(string tenantId) =>
        _products.TryGetValue(tenantId, out var p) ? p : new();

    public List<TenantUser> GetUsers(string tenantId) =>
        _users.TryGetValue(tenantId, out var u) ? u : new();

    public TenantProduct AddProduct(string tenantId, TenantProduct p)
    {
        if (!_products.ContainsKey(tenantId)) _products[tenantId] = new();
        p = p with { Id = _nextProductId++, TenantId = tenantId };
        _products[tenantId].Add(p);
        return p;
    }
}

class MultiTenantApplication
{
    static void Main()
    {
        Console.WriteLine("=== Multi-tenant Application (Basic) ===\n");

        var tenants = new List<Tenant>
        {
            new("tenant-alpha", "Alpha Corp",  "alpha.myapp.com",  "alpha",  true),
            new("tenant-beta",  "Beta Ltd",    "beta.myapp.com",   "beta",   true),
            new("tenant-gamma", "Gamma Inc",   "gamma.myapp.com",  "gamma",  false),
        };

        var resolver = new TenantResolver(tenants);
        var ctx      = new TenantContext();
        var store    = new TenantDataStore();

        store.SeedTenant("tenant-alpha",
            new[] {
                new TenantProduct(0, "tenant-alpha", "Laptop",  75000),
                new TenantProduct(0, "tenant-alpha", "Mouse",   1500)
            },
            new[] {
                new TenantUser("u1", "tenant-alpha", "alice", "Admin"),
                new TenantUser("u2", "tenant-alpha", "bob",   "User")
            });

        store.SeedTenant("tenant-beta",
            new[] {
                new TenantProduct(0, "tenant-beta", "Desk",   12000),
                new TenantProduct(0, "tenant-beta", "Chair",  8000)
            },
            new[] {
                new TenantUser("u3", "tenant-beta", "charlie", "Admin")
            });

        PrintTenantInfo(tenants);
        SimulateRequests(resolver, ctx, store);
        PrintIsolationStrategies();
    }

    static void PrintTenantInfo(List<Tenant> tenants)
    {
        Console.WriteLine("─── Registered Tenants ───────────────────────────");
        foreach (var t in tenants)
            Console.WriteLine($"  [{(t.IsActive ? "Active" : "Inactive")}] {t.Name,-14} domain:{t.Domain,-22} schema:{t.DbSchema}");
        Console.WriteLine();
    }

    static void SimulateRequests(TenantResolver resolver, TenantContext ctx, TenantDataStore store)
    {
        Console.WriteLine("─── Simulated HTTP Requests ──────────────────────");

        var requests = new[]
        {
            ("alpha.myapp.com", "GET /products"),
            ("beta.myapp.com",  "GET /products"),
            ("gamma.myapp.com", "GET /products"),
            ("unknown.com",     "GET /products"),
        };

        foreach (var (host, path) in requests)
        {
            var tenant = resolver.ResolveByDomain(host);
            Console.WriteLine($"\n  Host: {host}  {path}");
            if (tenant is null) { Console.WriteLine("    => 404 Tenant not found or inactive."); continue; }

            ctx.Set(tenant);
            Console.WriteLine($"    Resolved Tenant: {tenant.Name} (schema: {tenant.DbSchema})");
            var products = store.GetProducts(tenant.Id);
            foreach (var p in products)
                Console.WriteLine($"    Product [{p.Id}]: {p.Name} - Rs.{p.Price}");
            ctx.Clear();
        }

        Console.WriteLine("\n─── Tenant Header Resolution ─────────────────────");
        var t2 = resolver.ResolveByHeader("tenant-beta");
        Console.WriteLine($"  X-Tenant-Id: tenant-beta => {t2?.Name}");
        if (t2 != null)
            foreach (var u in store.GetUsers(t2.Id))
                Console.WriteLine($"    User: {u.Username} ({u.Role})");
    }

    static void PrintIsolationStrategies()
    {
        Console.WriteLine("\n─── Multi-tenancy Isolation Strategies ───────────");
        Console.WriteLine("  1. Separate Database  : Each tenant has its own DB (strongest isolation)");
        Console.WriteLine("  2. Separate Schema    : Shared DB, separate schemas (good balance)");
        Console.WriteLine("  3. Shared Schema      : Single DB + TenantId column (simplest, cheapest)");
        Console.WriteLine("  4. Hybrid             : Large tenants get own DB, small ones share\n");
        Console.WriteLine("  Tenant Resolution Methods:");
        Console.WriteLine("    - Subdomain : tenant1.app.com");
        Console.WriteLine("    - Header    : X-Tenant-Id: tenant1");
        Console.WriteLine("    - JWT Claim : { tenantId: \"tenant1\" }");
        Console.WriteLine("    - Path      : /tenant1/api/products");
    }
}
