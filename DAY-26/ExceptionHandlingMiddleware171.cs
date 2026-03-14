// Exception Handling Middleware in ASP.NET Core

// Simulated demo — pipeline + middleware pattern (no web server)
using System;
using System.Text.Json;
using System.Net;

class ExceptionHandlingMiddlewareDemo
{
    // Simulated HTTP context
    record SimContext(string Path)
    {
        public int    StatusCode { get; set; } = 200;
        public string Body      { get; set; } = "";
    }

    // Middleware: catches exceptions, writes error JSON
    static void GlobalExceptionMiddleware(SimContext ctx, Action<SimContext> next)
    {
        try
        {
            next(ctx);
        }
        catch (Exception ex)
        {
            var (code, msg) = ex switch
            {
                KeyNotFoundException        => (404, ex.Message),
                UnauthorizedAccessException => (401, ex.Message),
                ArgumentException           => (400, ex.Message),
                _                           => (500, "An unexpected error occurred.")
            };

            ctx.StatusCode = code;
            ctx.Body = JsonSerializer.Serialize(new
            {
                statusCode = code,
                message    = msg,
                timestamp  = DateTime.UtcNow.ToString("o")
            });
        }
    }

    // Simulated route handler
    static void RouteHandler(SimContext ctx)
    {
        ctx.Body = ctx.Path switch
        {
            "/ok"           => "All good!",
            "/not-found"    => throw new KeyNotFoundException("Item not found."),
            "/bad-request"  => throw new ArgumentException("Invalid input."),
            "/unauthorized" => throw new UnauthorizedAccessException("Access denied."),
            "/server-error" => throw new Exception("Something blew up."),
            _               => throw new KeyNotFoundException("Route not found.")
        };
    }

    static void Main()
    {
        Console.WriteLine("=== Exception Handling Middleware (simulated) ===\n");

        string[] routes = { "/ok", "/not-found", "/bad-request", "/unauthorized", "/server-error" };

        foreach (var path in routes)
        {
            var ctx = new SimContext(path);
            GlobalExceptionMiddleware(ctx, RouteHandler);
            Console.WriteLine($"  {ctx.StatusCode}  {path,-18} => {ctx.Body}");
        }
    }
}
