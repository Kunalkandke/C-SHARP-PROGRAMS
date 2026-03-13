// REST API with Role-based Authorization

// Simulated demo — role-based access control logic (no web server)
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Text.Json;

class RestApiRoleBasedAuthorization
{
    const string SecretKey = "MySuperSecretKey_12345_ABCDEF!@#";
    const string Issuer    = "MyApp";

    // Fake user store
    static readonly Dictionary<string, (string Password, string Role)> Users = new()
    {
        ["admin"] = ("pass123",  "Admin"),
        ["alice"] = ("alice123", "User"),
    };

    static void Main()
    {
        Console.WriteLine("=== REST API with Role-based Authorization (simulated) ===\n");

        // Admin login
        string? adminToken = Login("admin", "pass123");
        Console.WriteLine($"POST /login  (admin) => token: {adminToken?[..30]}...\n");

        // User login
        string? userToken = Login("alice", "alice123");
        Console.WriteLine($"POST /login  (alice) => token: {userToken?[..30]}...\n");

        // /user-data — both can access
        Console.WriteLine("GET /user-data");
        Console.WriteLine($"  [Admin] => {AccessEndpoint(adminToken!, "User", "Admin")}");
        Console.WriteLine($"  [User]  => {AccessEndpoint(userToken!,  "User", "Admin")}\n");

        // /admin-panel — Admin only
        Console.WriteLine("GET /admin-panel");
        Console.WriteLine($"  [Admin] => {AccessEndpoint(adminToken!, "Admin")}");
        Console.WriteLine($"  [User]  => {AccessEndpoint(userToken!,  "Admin")}\n");

        // DELETE /admin/delete/5 — Admin only
        Console.WriteLine("DELETE /admin/delete/5");
        Console.WriteLine($"  [Admin] => {DeleteEndpoint(adminToken!, 5)}");
        Console.WriteLine($"  [User]  => {DeleteEndpoint(userToken!,  5)}");
    }

    static string AccessEndpoint(string token, params string[] requiredRoles)
    {
        var (name, role) = ExtractClaims(token);
        if (name == null) return "401 Unauthorized — invalid token";
        foreach (var r in requiredRoles)
            if (role == r) return $"200 OK — Hello {name} ({role})! Access granted.";
        return $"403 Forbidden — {role} role not allowed here.";
    }

    static string DeleteEndpoint(string token, int id)
    {
        var (name, role) = ExtractClaims(token);
        if (name == null)   return "401 Unauthorized";
        if (role != "Admin") return $"403 Forbidden — {role} cannot delete records.";
        return $"200 OK — Admin '{name}' deleted record {id}.";
    }

    static string? Login(string username, string password)
    {
        if (!Users.TryGetValue(username, out var info) || info.Password != password) return null;
        var header  = Base64Url(JsonSerializer.Serialize(new { alg = "HS256", typ = "JWT" }));
        var payload = Base64Url(JsonSerializer.Serialize(new
        {
            iss  = Issuer,
            sub  = username,
            role = info.Role,
            exp  = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds()
        }));
        string sig = Sign($"{header}.{payload}", SecretKey);
        return $"{header}.{payload}.{sig}";
    }

    static (string? Name, string? Role) ExtractClaims(string token)
    {
        var parts = token.Split('.');
        if (parts.Length != 3) return (null, null);
        string expectedSig = Sign($"{parts[0]}.{parts[1]}", SecretKey);
        if (expectedSig != parts[2]) return (null, null);
        string json = Encoding.UTF8.GetString(Convert.FromBase64String(PadBase64(parts[1])));
        var doc = JsonDocument.Parse(json);
        return (doc.RootElement.GetProperty("sub").GetString(),
                doc.RootElement.GetProperty("role").GetString());
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
