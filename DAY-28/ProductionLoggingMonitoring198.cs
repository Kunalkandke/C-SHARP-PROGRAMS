// Production-grade Logging & Monitoring

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading;

enum LogLevel { Trace, Debug, Information, Warning, Error, Critical }

class LogEntry
{
    public DateTime  Timestamp   { get; init; } = DateTime.UtcNow;
    public LogLevel  Level       { get; init; }
    public string    Category    { get; init; } = "";
    public string    Message     { get; init; } = "";
    public string?   TraceId     { get; init; }
    public Dictionary<string, object> Properties { get; init; } = new();
    public Exception? Exception  { get; init; }
}

class StructuredLogger
{
    readonly string _category;
    readonly List<LogEntry> _sink;
    readonly LogLevel _minLevel;

    public StructuredLogger(string category, List<LogEntry> sink, LogLevel minLevel = LogLevel.Debug)
    {
        _category = category;
        _sink     = sink;
        _minLevel = minLevel;
    }

    public void Log(LogLevel level, string message, Dictionary<string, object>? props = null, Exception? ex = null)
    {
        if (level < _minLevel) return;
        var entry = new LogEntry
        {
            Level      = level,
            Category   = _category,
            Message    = message,
            TraceId    = Activity.Current?.TraceId.ToString()?[..8],
            Properties = props ?? new(),
            Exception  = ex
        };
        _sink.Add(entry);

        string color = level switch
        {
            LogLevel.Error or LogLevel.Critical => "\x1b[31m",
            LogLevel.Warning                    => "\x1b[33m",
            LogLevel.Information                => "\x1b[32m",
            _                                   => "\x1b[37m"
        };
        string propsStr = props?.Count > 0 ? " " + JsonSerializer.Serialize(props) : "";
        Console.WriteLine($"{color}  [{entry.Timestamp:HH:mm:ss.fff}] [{level,-12}] [{_category}] {message}{propsStr}{(ex != null ? $" | EX: {ex.Message}" : "")}\x1b[0m");
    }

    public void Trace(string msg, Dictionary<string, object>? p = null)       => Log(LogLevel.Trace,       msg, p);
    public void Debug(string msg, Dictionary<string, object>? p = null)       => Log(LogLevel.Debug,       msg, p);
    public void Info(string msg,  Dictionary<string, object>? p = null)       => Log(LogLevel.Information, msg, p);
    public void Warn(string msg,  Dictionary<string, object>? p = null)       => Log(LogLevel.Warning,     msg, p);
    public void Error(string msg, Exception? ex = null, Dictionary<string, object>? p = null) => Log(LogLevel.Error, msg, p, ex);
    public void Critical(string msg, Exception? ex = null)                    => Log(LogLevel.Critical,    msg, null, ex);
}

class MetricsCollector
{
    readonly Dictionary<string, List<double>> _metrics = new();

    public void Record(string name, double value)
    {
        if (!_metrics.ContainsKey(name)) _metrics[name] = new();
        _metrics[name].Add(value);
    }

    public void PrintSummary()
    {
        Console.WriteLine("\n─── Metrics Summary ──────────────────────────────");
        Console.WriteLine($"  {"Metric",-40} {"Count",6} {"Avg",8} {"P95",8} {"Max",8}");
        foreach (var (name, values) in _metrics)
        {
            if (values.Count == 0) continue;
            var sorted = values.OrderBy(v => v).ToList();
            double avg = values.Average();
            double p95 = sorted[(int)(sorted.Count * 0.95)];
            double max = sorted.Last();
            Console.WriteLine($"  {name,-40} {values.Count,6} {avg,7:F1}ms {p95,7:F1}ms {max,7:F1}ms");
        }
    }
}

class ProductionLoggingMonitoring
{
    static readonly List<LogEntry> LogSink = new();
    static readonly MetricsCollector Metrics = new();
    static readonly Random Rng = new(42);

    static void Main()
    {
        Console.WriteLine("=== Production-grade Logging & Monitoring ===\n");

        PrintLoggingSetup();
        RunApplicationDemo();
        PrintLogAnalysis();
        Metrics.PrintSummary();
        PrintAlertRules();
    }

    static void PrintLoggingSetup()
    {
        Console.WriteLine("─── Serilog Production Setup ─────────────────────");
        Console.WriteLine(@"  // Program.cs
  Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Information()
      .MinimumLevel.Override(""Microsoft"", LogEventLevel.Warning)
      .Enrich.FromLogContext()
      .Enrich.WithMachineName()
      .Enrich.WithEnvironmentName()
      .WriteTo.Console(outputTemplate: ""[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"")
      .WriteTo.File(""logs/app-.log"", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30)
      .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces)
      .CreateLogger();");
        Console.WriteLine();
    }

    static void RunApplicationDemo()
    {
        Console.WriteLine("─── Application Log Output ───────────────────────");

        var httpLog  = new StructuredLogger("HttpPipeline",  LogSink);
        var dbLog    = new StructuredLogger("Database",      LogSink);
        var authLog  = new StructuredLogger("Auth",          LogSink);
        var orderLog = new StructuredLogger("OrderService",  LogSink);

        string[] endpoints  = { "GET /api/products", "POST /api/orders", "GET /api/users/1", "DELETE /api/orders/5" };
        int[]    statusCodes = { 200, 201, 200, 403 };

        for (int i = 0; i < endpoints.Length; i++)
        {
            var sw = Stopwatch.StartNew();
            Thread.Sleep(Rng.Next(10, 80));
            sw.Stop();
            double ms = sw.Elapsed.TotalMilliseconds;
            Metrics.Record("http.request.duration", ms);

            httpLog.Info($"Request {endpoints[i]} => {statusCodes[i]}",
                new() { ["method"] = endpoints[i].Split(' ')[0], ["path"] = endpoints[i].Split(' ')[1],
                        ["status"] = statusCodes[i], ["durationMs"] = ms });
        }

        authLog.Warn("Failed login attempt", new() { ["username"] = "unknown_user", ["ip"] = "192.168.1.42", ["attempts"] = 3 });
        authLog.Warn("Failed login attempt", new() { ["username"] = "unknown_user", ["ip"] = "192.168.1.42", ["attempts"] = 4 });
        authLog.Error("Account locked after 5 failed attempts", null, new() { ["username"] = "unknown_user" });

        for (int i = 0; i < 5; i++)
        {
            var sw = Stopwatch.StartNew();
            Thread.Sleep(Rng.Next(5, 30));
            sw.Stop();
            Metrics.Record("db.query.duration", sw.Elapsed.TotalMilliseconds);
            dbLog.Debug($"Query executed", new() { ["query"] = "SELECT Products", ["rows"] = Rng.Next(1, 100), ["ms"] = sw.Elapsed.TotalMilliseconds });
        }

        orderLog.Info("Order placed", new() { ["orderId"] = 1001, ["customer"] = "alice@example.com", ["total"] = 75000 });

        try { throw new InvalidOperationException("Payment gateway timeout after 30s"); }
        catch (Exception ex)
        {
            orderLog.Error("Order payment failed", ex, new() { ["orderId"] = 1002, ["gateway"] = "Stripe" });
            Metrics.Record("payment.failures", 1);
        }

        orderLog.Critical("Database connection pool exhausted — service degraded");
    }

    static void PrintLogAnalysis()
    {
        Console.WriteLine("\n─── Log Analysis ─────────────────────────────────");
        var byLevel = LogSink.GroupBy(e => e.Level);
        foreach (var g in byLevel.OrderBy(g => g.Key))
            Console.WriteLine($"  {g.Key,-14}: {g.Count()} entries");

        Console.WriteLine("\n  Errors:");
        foreach (var e in LogSink.Where(e => e.Level >= LogLevel.Error))
            Console.WriteLine($"    [{e.Level}] {e.Category}: {e.Message}");
    }

    static void PrintAlertRules()
    {
        Console.WriteLine("\n─── Alert Rules (Application Insights / Grafana) ─");
        string[] rules =
        {
            "Error rate > 1%     for 5 min  → PagerDuty alert",
            "P95 latency > 500ms for 5 min  → Slack notification",
            "CPU > 80%           for 10 min → Auto-scale trigger",
            "Memory > 90%        for 5 min  → Restart + alert",
            "Failed logins > 10  per minute → Security alert",
            "DB connection pool > 90%       → Warning alert",
        };
        foreach (var r in rules) Console.WriteLine($"  • {r}");
    }
}
