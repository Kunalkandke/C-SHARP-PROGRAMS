// Application Performance Monitoring

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

class MetricEntry
{
    public string   Name      { get; set; } = "";
    public double   Value     { get; set; }
    public string   Unit      { get; set; } = "";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

class PerformanceMonitor
{
    readonly List<MetricEntry> _metrics = new();
    readonly object _lock = new();

    public void Record(string name, double value, string unit = "ms")
    {
        lock (_lock)
            _metrics.Add(new MetricEntry { Name = name, Value = value, Unit = unit });
    }

    public IDisposable MeasureTime(string operation) => new Timer(this, operation);

    public void PrintReport()
    {
        Console.WriteLine("\n─── Performance Report ───────────────────────────");
        var groups = _metrics.GroupBy(m => m.Name);
        foreach (var g in groups)
        {
            double avg = g.Average(m => m.Value);
            double min = g.Min(m => m.Value);
            double max = g.Max(m => m.Value);
            string unit = g.First().Unit;
            Console.WriteLine($"  {g.Key,-35} Avg:{avg,7:F1}{unit}  Min:{min,6:F1}  Max:{max,6:F1}  Calls:{g.Count()}");
        }
    }

    class Timer : IDisposable
    {
        readonly PerformanceMonitor _monitor;
        readonly string _operation;
        readonly Stopwatch _sw = Stopwatch.StartNew();
        public Timer(PerformanceMonitor m, string op) { _monitor = m; _operation = op; }
        public void Dispose() { _sw.Stop(); _monitor.Record(_operation, _sw.Elapsed.TotalMilliseconds); }
    }
}

class ApplicationPerformanceMonitoring
{
    static readonly PerformanceMonitor Monitor = new();
    static readonly Random Rng = new(42);

    static void Main()
    {
        Console.WriteLine("=== Application Performance Monitoring ===\n");

        PrintAppInsightsSetup();
        RunSimulation();
        Monitor.PrintReport();
        PrintHealthCheck();
    }

    static void PrintAppInsightsSetup()
    {
        Console.WriteLine("─── Application Insights Setup ───────────────────");
        Console.WriteLine("  dotnet add package Microsoft.ApplicationInsights.AspNetCore\n");
        Console.WriteLine(@"  // Program.cs
  builder.Services.AddApplicationInsightsTelemetry(
      builder.Configuration[""ApplicationInsights:InstrumentationKey""]);

  // Tracks: requests, exceptions, dependencies, custom events, metrics
  // Dashboard: https://portal.azure.com → Application Insights");
        Console.WriteLine();
    }

    static void RunSimulation()
    {
        Console.WriteLine("─── Simulating Application Traffic ───────────────");

        for (int i = 0; i < 10; i++)
        {
            using (Monitor.MeasureTime("GET /api/products"))
                Thread.Sleep(Rng.Next(10, 60));

            using (Monitor.MeasureTime("GET /api/products/{id}"))
                Thread.Sleep(Rng.Next(5, 30));

            using (Monitor.MeasureTime("POST /api/orders"))
                Thread.Sleep(Rng.Next(30, 100));

            using (Monitor.MeasureTime("SQL: SELECT Products"))
                Thread.Sleep(Rng.Next(5, 25));

            using (Monitor.MeasureTime("SQL: INSERT Orders"))
                Thread.Sleep(Rng.Next(10, 40));

            Console.Write($"\r  Simulating requests... {i + 1}/10");
        }
        Console.WriteLine("\n  Done.\n");

        Console.WriteLine("─── Custom Metrics ───────────────────────────────");
        Monitor.Record("Active Users",     342,  " users");
        Monitor.Record("Cache Hit Rate",    87.4, "%");
        Monitor.Record("Error Rate",         0.3, "%");
        Monitor.Record("CPU Usage",         42.1, "%");
        Monitor.Record("Memory Usage (MB)", 256,  " MB");
        foreach (var kv in new[] {
            ("Active Users", "342 users"), ("Cache Hit Rate", "87.4%"),
            ("Error Rate", "0.3%"), ("CPU Usage", "42.1%"), ("Memory", "256 MB") })
            Console.WriteLine($"  {kv.Item1,-22}: {kv.Item2}");
    }

    static void PrintHealthCheck()
    {
        Console.WriteLine("\n─── Health Check Endpoints ───────────────────────");
        Console.WriteLine(@"  // Program.cs
  builder.Services.AddHealthChecks()
      .AddSqlServer(connStr, name: ""database"")
      .AddRedis(redisConn, name: ""redis"")
      .AddUrlGroup(new Uri(""https://external-api.com/health""), name: ""external-api"");

  app.MapHealthChecks(""/health"");
  app.MapHealthChecks(""/health/detail"", new HealthCheckOptions {
      ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
  });

  // Simulated:
  GET /health        => Healthy
  GET /health/detail => { database: Healthy, redis: Healthy, external-api: Degraded }");
    }
}
