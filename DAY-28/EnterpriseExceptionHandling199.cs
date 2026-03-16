// Enterprise Exception Handling Strategy

using System;
using System.Collections.Generic;
using System.Text.Json;

// ── Custom Exception Hierarchy ─────────────────────────────────────────────
class AppException : Exception
{
    public string Code      { get; }
    public int    HttpStatus { get; }
    public AppException(string code, string message, int httpStatus = 500) : base(message)
    { Code = code; HttpStatus = httpStatus; }
}

class NotFoundException       : AppException { public NotFoundException(string resource, object id) : base("NOT_FOUND", $"{resource} with id '{id}' was not found.", 404) { } }
class ValidationException     : AppException
{
    public Dictionary<string, string[]> Errors { get; }
    public ValidationException(Dictionary<string, string[]> errors) : base("VALIDATION_FAILED", "One or more validation errors occurred.", 400)
    { Errors = errors; }
}
class UnauthorizedException   : AppException { public UnauthorizedException(string msg = "Authentication required.") : base("UNAUTHORIZED", msg, 401) { } }
class ForbiddenException      : AppException { public ForbiddenException(string msg = "You do not have permission.") : base("FORBIDDEN", msg, 403) { } }
class ConflictException       : AppException { public ConflictException(string resource, string detail) : base("CONFLICT", $"{resource}: {detail}", 409) { } }
class ExternalServiceException: AppException { public ExternalServiceException(string service, string detail) : base("EXTERNAL_SERVICE_ERROR", $"{service} failed: {detail}", 502) { } }

// ── Problem Details response (RFC 7807) ────────────────────────────────────
class ProblemDetails
{
    public int    Status    { get; set; }
    public string Type      { get; set; } = "";
    public string Title     { get; set; } = "";
    public string Detail    { get; set; } = "";
    public string TraceId   { get; set; } = "";
    public Dictionary<string, object>? Extensions { get; set; }
}

// ── Global Exception Handler ───────────────────────────────────────────────
class GlobalExceptionHandler
{
    static readonly Dictionary<Type, string> TypeMap = new()
    {
        [typeof(NotFoundException)]        = "https://errors.myapi.com/not-found",
        [typeof(ValidationException)]      = "https://errors.myapi.com/validation",
        [typeof(UnauthorizedException)]    = "https://errors.myapi.com/unauthorized",
        [typeof(ForbiddenException)]       = "https://errors.myapi.com/forbidden",
        [typeof(ConflictException)]        = "https://errors.myapi.com/conflict",
        [typeof(ExternalServiceException)] = "https://errors.myapi.com/external-service",
    };

    public ProblemDetails Handle(Exception ex, string traceId)
    {
        var pd = new ProblemDetails { TraceId = traceId };

        if (ex is AppException appEx)
        {
            pd.Status = appEx.HttpStatus;
            pd.Type   = TypeMap.GetValueOrDefault(ex.GetType(), "https://errors.myapi.com/error");
            pd.Title  = appEx.Code;
            pd.Detail = appEx.Message;

            if (appEx is ValidationException valEx)
                pd.Extensions = new() { ["errors"] = valEx.Errors };
        }
        else
        {
            pd.Status = 500;
            pd.Type   = "https://errors.myapi.com/internal-server-error";
            pd.Title  = "INTERNAL_SERVER_ERROR";
            pd.Detail = "An unexpected error occurred. Please try again later.";
            // Log full ex details internally — never expose stack trace to client
        }

        return pd;
    }
}

// ── Retry Policy ───────────────────────────────────────────────────────────
class RetryPolicy
{
    public static T Execute<T>(Func<T> action, int maxRetries = 3, int baseDelayMs = 100)
    {
        int attempt = 0;
        while (true)
        {
            try
            {
                return action();
            }
            catch (ExternalServiceException ex) when (attempt < maxRetries)
            {
                attempt++;
                int delay = baseDelayMs * (int)Math.Pow(2, attempt); // exponential backoff
                Console.WriteLine($"    [Retry {attempt}/{maxRetries}] {ex.Message} — retrying in {delay}ms");
                System.Threading.Thread.Sleep(delay);
            }
        }
    }
}

// ── Application Layer (uses exceptions) ────────────────────────────────────
class OrderService
{
    static readonly List<(int Id, string Name)> _orders = new() { (1, "Order#1"), (2, "Order#2") };
    static int _callCount = 0;

    public (int Id, string Name) GetOrder(int id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order == default) throw new NotFoundException("Order", id);
        return order;
    }

    public void CreateOrder(string customer, int quantity)
    {
        var errors = new Dictionary<string, string[]>();
        if (string.IsNullOrWhiteSpace(customer)) errors["customer"] = new[] { "Customer name is required." };
        if (quantity <= 0) errors["quantity"] = new[] { "Quantity must be greater than 0." };
        if (quantity > 1000) errors["quantity"] = new[] { "Quantity cannot exceed 1000." };
        if (errors.Count > 0) throw new ValidationException(errors);

        if (_orders.Any(o => o.Name == customer)) throw new ConflictException("Order", "Duplicate order for this customer.");
        _orders.Add((_orders.Count + 1, customer));
    }

    public string CallExternalPayment()
    {
        _callCount++;
        if (_callCount < 3) throw new ExternalServiceException("PaymentGateway", "Connection timeout");
        return "Payment processed successfully.";
    }
}

class EnterpriseExceptionHandlingStrategy
{
    static readonly GlobalExceptionHandler Handler = new();

    static void Main()
    {
        Console.WriteLine("=== Enterprise Exception Handling Strategy ===\n");

        var service = new OrderService();

        Simulate("GetOrder — valid",          () => { var o = service.GetOrder(1); Console.WriteLine($"  Found: {o.Name}"); });
        Simulate("GetOrder — not found",      () => service.GetOrder(99));
        Simulate("CreateOrder — valid",       () => { service.CreateOrder("Alice", 5); Console.WriteLine("  Order created."); });
        Simulate("CreateOrder — validation",  () => service.CreateOrder("", -3));
        Simulate("CreateOrder — conflict",    () => service.CreateOrder("Alice", 5));
        Simulate("Admin access — forbidden",  () => throw new ForbiddenException("Admin role required."));
        Simulate("Unauthenticated request",   () => throw new UnauthorizedException());

        Console.WriteLine("\n─── Retry Policy (exponential backoff) ───────────");
        try
        {
            string result = RetryPolicy.Execute(() => service.CallExternalPayment(), maxRetries: 3, baseDelayMs: 50);
            Console.WriteLine($"  Success: {result}");
        }
        catch (ExternalServiceException ex)
        {
            Console.WriteLine($"  All retries exhausted: {ex.Message}");
        }

        PrintMiddlewareNote();
    }

    static void Simulate(string label, Action action)
    {
        Console.WriteLine($"\n─── {label}");
        try
        {
            action();
        }
        catch (Exception ex)
        {
            var pd = Handler.Handle(ex, "trace-abc123");
            Console.WriteLine($"  HTTP {pd.Status} | {pd.Title}");
            Console.WriteLine($"  Detail : {pd.Detail}");
            if (pd.Extensions?.TryGetValue("errors", out var errs) == true)
                Console.WriteLine($"  Errors : {JsonSerializer.Serialize(errs)}");
        }
    }

    static void PrintMiddlewareNote()
    {
        Console.WriteLine("\n─── ASP.NET Core Middleware Integration ──────────");
        Console.WriteLine(@"  // Program.cs
  app.UseExceptionHandler(errApp => errApp.Run(async ctx =>
  {
      var ex      = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
      var handler = ctx.RequestServices.GetRequiredService<GlobalExceptionHandler>();
      var pd      = handler.Handle(ex!, Activity.Current?.TraceId.ToString() ?? """");
      ctx.Response.StatusCode  = pd.Status;
      ctx.Response.ContentType = ""application/problem+json"";
      await ctx.Response.WriteAsJsonAsync(pd);
  }));");
    }
}
