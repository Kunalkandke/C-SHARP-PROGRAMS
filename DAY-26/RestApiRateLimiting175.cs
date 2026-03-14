// REST API with Rate Limiting


// Simulated demo — token bucket algorithm (no web server)
using System;
using System.Collections.Generic;
using System.Threading;

class TokenBucket
{
    readonly int    _capacity;
    readonly int    _refillAmount;
    readonly int    _refillIntervalMs;
    int             _tokens;
    DateTime        _lastRefill;
    readonly object _lock = new();

    public TokenBucket(int capacity, int refillAmount, int refillIntervalMs)
    {
        _capacity          = capacity;
        _refillAmount      = refillAmount;
        _refillIntervalMs  = refillIntervalMs;
        _tokens            = capacity;
        _lastRefill        = DateTime.UtcNow;
    }

    public bool TryConsume()
    {
        lock (_lock)
        {
            // Refill tokens based on elapsed time
            int elapsed = (int)(DateTime.UtcNow - _lastRefill).TotalMilliseconds;
            if (elapsed >= _refillIntervalMs)
            {
                int periods = elapsed / _refillIntervalMs;
                _tokens     = Math.Min(_capacity, _tokens + periods * _refillAmount);
                _lastRefill = DateTime.UtcNow;
            }

            if (_tokens <= 0) return false;
            _tokens--;
            return true;
        }
    }
}

class FixedWindowLimiter
{
    readonly int      _limit;
    readonly TimeSpan _window;
    int               _count;
    DateTime          _windowStart;

    public FixedWindowLimiter(int limit, TimeSpan window)
    {
        _limit       = limit;
        _window      = window;
        _windowStart = DateTime.UtcNow;
    }

    public bool TryConsume()
    {
        if (DateTime.UtcNow - _windowStart > _window) { _count = 0; _windowStart = DateTime.UtcNow; }
        if (_count >= _limit) return false;
        _count++;
        return true;
    }
}

class RestApiRateLimiting
{
    static void Main()
    {
        Console.WriteLine("=== REST API with Rate Limiting (simulated) ===\n");

        Console.WriteLine("--- Token Bucket (capacity=5, refill=2 every 500ms) ---");
        var bucket = new TokenBucket(capacity: 5, refillAmount: 2, refillIntervalMs: 500);
        SimulateRequests("POST /api/orders", bucket.TryConsume, 8, 50);

        Console.WriteLine("\n--- Fixed Window (5 requests per 1 sec) ---");
        var window = new FixedWindowLimiter(limit: 5, window: TimeSpan.FromSeconds(1));
        SimulateRequests("GET /api/products", window.TryConsume, 8, 0);
    }

    static void SimulateRequests(string endpoint, Func<bool> limiter, int count, int delayMs)
    {
        for (int i = 1; i <= count; i++)
        {
            bool allowed = limiter();
            string status = allowed ? "200 OK              " : "429 Too Many Requests";
            Console.WriteLine($"  Request {i,2}: {endpoint,-22} => {status}");
            if (delayMs > 0) Thread.Sleep(delayMs);
        }
    }
}
