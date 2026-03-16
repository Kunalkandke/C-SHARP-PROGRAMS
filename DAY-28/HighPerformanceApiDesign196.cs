// High-performance API Design

using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

record ProductRecord(int Id, string Name, decimal Price, int Stock);

class HighPerformanceApiDesign
{
    static readonly Random Rng = new(42);
    static readonly List<ProductRecord> Products = Enumerable.Range(1, 10_000)
        .Select(i => new ProductRecord(i, $"Product{i}", Rng.Next(100, 100_000), Rng.Next(0, 500)))
        .ToList();

    static void Main()
    {
        Console.WriteLine("=== High-performance API Design ===\n");

        PrintDesignPrinciples();
        BenchmarkLinqVsSpan();
        DemoResponseCaching();
        DemoObjectPooling();
        DemoAsyncStreamProcessing().GetAwaiter().GetResult();
        PrintAspNetCoreTips();
    }

    static void PrintDesignPrinciples()
    {
        Console.WriteLine("─── Key Principles ───────────────────────────────");
        string[] principles =
        {
            "Use async/await for all I/O (never block threads)",
            "Response caching: [ResponseCache] or IMemoryCache",
            "Output caching: cache full endpoint responses (.NET 7+)",
            "Use IAsyncEnumerable<T> for streaming large datasets",
            "Minimal API reduces overhead vs MVC controllers",
            "Use ArrayPool<T> / MemoryPool<T> to avoid allocations",
            "Prefer Span<T> / Memory<T> for zero-copy operations",
            "Compile-time JSON source generation (JsonSerializerContext)",
            "Use HTTP/2 and response compression (Brotli/Gzip)",
            "Database: async EF Core, projections (Select), no N+1 queries",
            "Connection pooling, read replicas for heavy read workloads",
        };
        foreach (var p in principles) Console.WriteLine($"  • {p}");
        Console.WriteLine();
    }

    static void BenchmarkLinqVsSpan()
    {
        Console.WriteLine("─── Benchmark: LINQ vs Span filtering (10k items) ─");

        var sw = Stopwatch.StartNew();
        var linqResult = Products.Where(p => p.Price > 50000 && p.Stock > 0).Select(p => p.Id).ToList();
        sw.Stop();
        Console.WriteLine($"  LINQ   : {sw.ElapsedMilliseconds,4} ms, {linqResult.Count} results");

        sw.Restart();
        var spanResult = new List<int>(linqResult.Count);
        var span = System.Runtime.InteropServices.CollectionsMarshal.AsSpan(Products);
        foreach (ref readonly var p in span)
            if (p.Price > 50000 && p.Stock > 0) spanResult.Add(p.Id);
        sw.Stop();
        Console.WriteLine($"  Span   : {sw.ElapsedMilliseconds,4} ms, {spanResult.Count} results (fewer allocations)");
        Console.WriteLine();
    }

    static void DemoResponseCaching()
    {
        Console.WriteLine("─── Response Caching Demo ────────────────────────");
        var cache = new Dictionary<string, (object Data, DateTime ExpiresAt)>();

        string GetProducts(string category)
        {
            string key = $"products:{category}";
            if (cache.TryGetValue(key, out var entry) && DateTime.UtcNow < entry.ExpiresAt)
            {
                Console.WriteLine($"  [CACHE HIT ] GET /products?cat={category}");
                return JsonSerializer.Serialize(entry.Data);
            }
            var sw = Stopwatch.StartNew();
            var data = Products.Where(p => p.Name.EndsWith(category)).Take(5).ToList();
            sw.Stop();
            cache[key] = (data, DateTime.UtcNow.AddSeconds(30));
            Console.WriteLine($"  [CACHE MISS] GET /products?cat={category} — DB: {sw.ElapsedMilliseconds}ms, cached 30s");
            return JsonSerializer.Serialize(data);
        }

        GetProducts("1"); GetProducts("1"); GetProducts("2"); GetProducts("1");
        Console.WriteLine();
    }

    static void DemoObjectPooling()
    {
        Console.WriteLine("─── ArrayPool<byte> (zero-allocation serialization) ─");
        byte[] rented = ArrayPool<byte>.Shared.Rent(4096);
        try
        {
            string json = JsonSerializer.Serialize(Products.Take(3));
            int written = Encoding.UTF8.GetBytes(json, 0, json.Length, rented, 0);
            Console.WriteLine($"  Rented 4096 bytes, wrote {written} bytes (no heap alloc for buffer)");
            Console.WriteLine($"  Payload: {Encoding.UTF8.GetString(rented, 0, Math.Min(written, 80))}...");
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rented);
            Console.WriteLine("  Buffer returned to pool.");
        }
        Console.WriteLine();
    }

    static async Task DemoAsyncStreamProcessing()
    {
        Console.WriteLine("─── IAsyncEnumerable<T> Streaming ────────────────");

        int count = 0;
        await foreach (var product in StreamProductsAsync(10))
        {
            if (count < 3) Console.WriteLine($"  Streamed: [{product.Id}] {product.Name} Rs.{product.Price}");
            count++;
        }
        Console.WriteLine($"  ... {count} products streamed without loading all into memory.");
        Console.WriteLine();
    }

    static async IAsyncEnumerable<ProductRecord> StreamProductsAsync(
        int take, [EnumeratorCancellation] CancellationToken ct = default)
    {
        foreach (var p in Products.Take(take))
        {
            await Task.Delay(1, ct);
            yield return p;
        }
    }

    static void PrintAspNetCoreTips()
    {
        Console.WriteLine("─── ASP.NET Core Perf Tips ───────────────────────");
        Console.WriteLine(@"  app.UseResponseCompression();   // Brotli/Gzip
  app.UseOutputCache();           // full response cache (.NET 7+)
  app.MapGet(""/stream"", () =>    // streaming response
      Results.Ok(StreamProductsAsync()));
  builder.Services.AddResponseCaching();
  // Compile-time JSON serialization:
  [JsonSerializable(typeof(List<ProductRecord>))]
  partial class AppJsonContext : JsonSerializerContext { }
  JsonSerializer.Serialize(data, AppJsonContext.Default.ListProductRecord);");
    }
}
