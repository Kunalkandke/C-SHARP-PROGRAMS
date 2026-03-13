// REST API with JWT Authentication


// ─── Testing with curl ────────────────────────────────────────────────────────
// 1. Login (get token):
//    curl -X POST https://localhost:5001/login \
//         -H "Content-Type: application/json" \
//         -d "{\"username\":\"admin\",\"password\":\"password123\"}"
//
// 2. Access protected endpoint with token:
//    curl https://localhost:5001/secure \
//         -H "Authorization: Bearer <YOUR_TOKEN_HERE>"
//
// 3. Without token => 401 Unauthorized

// Simulated demo — JWT generation and validation logic (no web server)
using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.Json;

class RestApiJwtAuthentication
{
    const string SecretKey = "MySuperSecretKey_12345_ABCDEF!@#";
    const string Issuer    = "MyApp";

    static void Main()
    {
        Console.WriteLine("=== REST API with JWT Authentication (simulated) ===\n");

        // Step 1 — Login
        Console.WriteLine("POST /login  {username: \"admin\", password: \"password123\"}");
        string? token = Login("admin", "password123");
        if (token == null) { Console.WriteLine("  401 Unauthorized"); return; }
        Console.WriteLine($"  200 OK => token: {token[..40]}...\n");

        // Step 2 — Access protected route with token
        Console.WriteLine("GET /secure  (with valid Bearer token)");
        string response = AccessSecureEndpoint(token);
        Console.WriteLine($"  {response}\n");

        // Step 3 — Access without token
        Console.WriteLine("GET /secure  (without token)");
        Console.WriteLine("  401 Unauthorized\n");

        // Step 4 — Access with tampered token
        Console.WriteLine("GET /secure  (with tampered token)");
        bool valid = ValidateToken(token + "tampered");
        Console.WriteLine(valid ? "  200 OK" : "  401 Unauthorized — token signature invalid");
    }

    // Build a simple HS256 JWT manually
    static string? Login(string username, string password)
    {
        if (username != "admin" || password != "password123") return null;

        var header  = Base64Url(JsonSerializer.Serialize(new { alg = "HS256", typ = "JWT" }));
        var payload = Base64Url(JsonSerializer.Serialize(new
        {
            iss  = Issuer,
            sub  = username,
            role = "Admin",
            exp  = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()
        }));

        string signature = Sign($"{header}.{payload}", SecretKey);
        return $"{header}.{payload}.{signature}";
    }

    static string AccessSecureEndpoint(string token)
    {
        if (!ValidateToken(token)) return "401 Unauthorized";
        string payload = Encoding.UTF8.GetString(Convert.FromBase64String(PadBase64(token.Split('.')[1])));
        var doc = JsonDocument.Parse(payload);
        string name = doc.RootElement.GetProperty("sub").GetString() ?? "unknown";
        return $"200 OK => Hello, {name}! You accessed a protected endpoint.";
    }

    static bool ValidateToken(string token)
    {
        var parts = token.Split('.');
        if (parts.Length != 3) return false;
        string expectedSig = Sign($"{parts[0]}.{parts[1]}", SecretKey);
        return expectedSig == parts[2];
    }

    static string Sign(string data, string secret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        return Base64Url(hmac.ComputeHash(Encoding.UTF8.GetBytes(data)));
    }

    static string Base64Url(string json) => Base64Url(Encoding.UTF8.GetBytes(json));
    static string Base64Url(byte[] data) =>
        Convert.ToBase64String(data).TrimEnd('=').Replace('+', '-').Replace('/', '_');

    static string PadBase64(string s)
    {
        s = s.Replace('-', '+').Replace('_', '/');
        return s.Length % 4 switch { 2 => s + "==", 3 => s + "=", _ => s };
    }
}
