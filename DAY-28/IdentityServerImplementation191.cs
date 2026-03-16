// Identity Server Implementation

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

record Client(string ClientId, string ClientSecret, string[] AllowedScopes, string[] RedirectUris);
record ApiResource(string Name, string[] Scopes);
record IdentityUser(string SubjectId, string Username, string PasswordHash, string[] Roles);

class TokenStore
{
    readonly Dictionary<string, (string Subject, string[] Scopes, DateTime Expiry)> _tokens = new();

    public string Issue(string subject, string[] scopes, TimeSpan lifetime)
    {
        string token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        _tokens[token] = (subject, scopes, DateTime.UtcNow.Add(lifetime));
        return token;
    }

    public (bool Valid, string Subject, string[] Scopes) Validate(string token)
    {
        if (_tokens.TryGetValue(token, out var t) && DateTime.UtcNow < t.Expiry)
            return (true, t.Subject, t.Scopes);
        return (false, "", Array.Empty<string>());
    }
}

class IdentityServer
{
    readonly List<Client>        _clients   = new();
    readonly List<IdentityUser>  _users     = new();
    readonly List<ApiResource>   _resources = new();
    readonly TokenStore          _tokens    = new();

    public void AddClient(Client c)      => _clients.Add(c);
    public void AddUser(IdentityUser u)  => _users.Add(u);
    public void AddResource(ApiResource r) => _resources.Add(r);

    static string HashPw(string pw) =>
        Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(pw)));

    public (string? AccessToken, string Error) Token_ResourceOwner(
        string clientId, string clientSecret, string username, string password, string scope)
    {
        var client = _clients.Find(c => c.ClientId == clientId && c.ClientSecret == clientSecret);
        if (client is null) return (null, "invalid_client");

        var user = _users.Find(u => u.Username == username && u.PasswordHash == HashPw(password));
        if (user is null) return (null, "invalid_grant");

        var scopes = scope.Split(' ');
        foreach (var s in scopes)
            if (!client.AllowedScopes.Contains(s)) return (null, $"scope '{s}' not allowed");

        string token = _tokens.Issue(user.SubjectId, scopes, TimeSpan.FromHours(1));
        return (token, "");
    }

    public (string? AccessToken, string Error) Token_ClientCredentials(
        string clientId, string clientSecret, string scope)
    {
        var client = _clients.Find(c => c.ClientId == clientId && c.ClientSecret == clientSecret);
        if (client is null) return (null, "invalid_client");

        var scopes = scope.Split(' ');
        foreach (var s in scopes)
            if (!client.AllowedScopes.Contains(s)) return (null, $"scope '{s}' not allowed");

        string token = _tokens.Issue(clientId, scopes, TimeSpan.FromMinutes(30));
        return (token, "");
    }

    public object? Introspect(string token)
    {
        var (valid, subject, scopes) = _tokens.Validate(token);
        if (!valid) return null;
        return new { active = true, sub = subject, scope = string.Join(" ", scopes), exp = DateTime.UtcNow.AddHours(1) };
    }
}

class IdentityServerImplementation
{
    static void Main()
    {
        Console.WriteLine("=== Identity Server Implementation ===\n");

        var ids = new IdentityServer();

        ids.AddClient(new("web_app",    "secret1", new[] { "openid", "profile", "api1" }, new[] { "https://app.example.com/callback" }));
        ids.AddClient(new("service_a",  "secret2", new[] { "api1", "api2" },              new[] { "" }));

        ids.AddUser(new("sub-001", "alice", Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("alice123"))), new[] { "Admin" }));
        ids.AddUser(new("sub-002", "bob",   Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes("bob456"))),   new[] { "User"  }));

        ids.AddResource(new("api1", new[] { "api1" }));
        ids.AddResource(new("api2", new[] { "api2" }));

        Console.WriteLine("─── Resource Owner Password Grant ────────────────");
        var (t1, e1) = ids.Token_ResourceOwner("web_app", "secret1", "alice", "alice123", "openid api1");
        Console.WriteLine($"  alice  valid creds  => {(t1 != null ? "access_token: " + t1[..16] + "..." : "Error: " + e1)}");

        var (t2, e2) = ids.Token_ResourceOwner("web_app", "secret1", "alice", "wrongpass", "openid api1");
        Console.WriteLine($"  alice  wrong pass   => {(t2 != null ? "token issued" : "Error: " + e2)}");

        var (t3, e3) = ids.Token_ResourceOwner("web_app", "secret1", "alice", "alice123", "openid api2");
        Console.WriteLine($"  alice  bad scope    => {(t3 != null ? "token issued" : "Error: " + e3)}");

        Console.WriteLine("\n─── Client Credentials Grant ─────────────────────");
        var (t4, e4) = ids.Token_ClientCredentials("service_a", "secret2", "api1 api2");
        Console.WriteLine($"  service_a  valid    => {(t4 != null ? "access_token: " + t4[..16] + "..." : "Error: " + e4)}");

        Console.WriteLine("\n─── Token Introspection ──────────────────────────");
        var info = ids.Introspect(t1!);
        Console.WriteLine(info is not null
            ? "  Token valid: " + JsonSerializer.Serialize(info)
            : "  Token invalid/expired");

        Console.WriteLine("\n─── ASP.NET Core Integration Notes ──────────────");
        Console.WriteLine("  NuGet: Duende.IdentityServer  (or OpenIddict)");
        Console.WriteLine("  builder.Services.AddIdentityServer()");
        Console.WriteLine("           .AddInMemoryClients(Config.Clients)");
        Console.WriteLine("           .AddInMemoryApiResources(Config.ApiResources)");
        Console.WriteLine("           .AddAspNetIdentity<ApplicationUser>();");
        Console.WriteLine("  app.UseIdentityServer();");
    }
}
