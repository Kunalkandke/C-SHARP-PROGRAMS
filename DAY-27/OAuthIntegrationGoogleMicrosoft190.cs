// OAuth Integration (Google / Microsoft)

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

record OAuthUser(string Id, string Email, string Name, string Provider, string AccessToken);

class OAuthProvider
{
    public string Name         { get; init; } = "";
    public string ClientId     { get; init; } = "";
    public string AuthEndpoint { get; init; } = "";
    public string TokenEndpoint{ get; init; } = "";
    public string[] Scopes     { get; init; } = Array.Empty<string>();
}

class OAuthIntegrationDemo
{
    static readonly Dictionary<string, OAuthUser> UserStore = new();

    static readonly OAuthProvider Google = new()
    {
        Name          = "Google",
        ClientId      = "YOUR_GOOGLE_CLIENT_ID.apps.googleusercontent.com",
        AuthEndpoint  = "https://accounts.google.com/o/oauth2/v2/auth",
        TokenEndpoint = "https://oauth2.googleapis.com/token",
        Scopes        = new[] { "openid", "email", "profile" }
    };

    static readonly OAuthProvider Microsoft = new()
    {
        Name          = "Microsoft",
        ClientId      = "YOUR_MICROSOFT_CLIENT_ID",
        AuthEndpoint  = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize",
        TokenEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token",
        Scopes        = new[] { "openid", "email", "profile", "User.Read" }
    };

    static void Main()
    {
        Console.WriteLine("=== OAuth Integration (Google / Microsoft) ===\n");

        PrintAspNetCoreSetup();
        PrintOAuthFlow();
        SimulateOAuthLogin(Google,    "alice@gmail.com",       "Alice Smith");
        SimulateOAuthLogin(Microsoft, "bob@outlook.com",       "Bob Jones");
        SimulateOAuthLogin(Google,    "charlie@gmail.com",     "Charlie Brown");

        Console.WriteLine("\n─── Registered Users ─────────────────────────────");
        foreach (var u in UserStore.Values)
            Console.WriteLine($"  [{u.Provider,-10}] {u.Email,-30} {u.Name}");
    }

    static void PrintAspNetCoreSetup()
    {
        Console.WriteLine("─── NuGet Packages ───────────────────────────────");
        Console.WriteLine("  dotnet add package Microsoft.AspNetCore.Authentication.Google");
        Console.WriteLine("  dotnet add package Microsoft.AspNetCore.Authentication.MicrosoftAccount\n");

        Console.WriteLine("─── Program.cs Setup ─────────────────────────────");
        Console.WriteLine(@"  builder.Services.AddAuthentication(options => {
      options.DefaultScheme          = CookieAuthenticationDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
  })
  .AddCookie()
  .AddGoogle(opt => {
      opt.ClientId     = builder.Configuration[""Google:ClientId""];
      opt.ClientSecret = builder.Configuration[""Google:ClientSecret""];
  })
  .AddMicrosoftAccount(opt => {
      opt.ClientId     = builder.Configuration[""Microsoft:ClientId""];
      opt.ClientSecret = builder.Configuration[""Microsoft:ClientSecret""];
  });

  app.UseAuthentication();
  app.UseAuthorization();

  // Login endpoints auto-generated:
  // GET /signin-google    — redirects to Google consent screen
  // GET /signin-microsoft — redirects to Microsoft consent screen
  // GET /signout          — clears cookie and logs out
");
    }

    static void PrintOAuthFlow()
    {
        Console.WriteLine("─── OAuth 2.0 + OIDC Flow ────────────────────────");
        string[] steps =
        {
            "1. User clicks 'Login with Google'",
            "2. App redirects → Google Auth with client_id, scope, state, redirect_uri",
            "3. User grants consent on Google's page",
            "4. Google redirects back with ?code=AUTH_CODE&state=...",
            "5. App exchanges code for access_token + id_token (POST /token)",
            "6. App decodes id_token (JWT) → gets user email, name, sub",
            "7. App creates session / issues its own JWT",
            "8. User is logged in",
        };
        foreach (var s in steps) Console.WriteLine($"  {s}");
        Console.WriteLine();
    }

    static void SimulateOAuthLogin(OAuthProvider provider, string email, string name)
    {
        Console.WriteLine($"─── OAuth Login via {provider.Name} ──────────────────────");

        string state        = GenerateRandom(8);
        string authCode     = GenerateRandom(16);
        string accessToken  = GenerateRandom(32);
        string userId       = HashString(email + provider.Name)[..12];

        Console.WriteLine($"  1. Redirect  → {provider.AuthEndpoint}?client_id=...&scope={string.Join("+", provider.Scopes)}&state={state}");
        Console.WriteLine($"  2. Consent   → User grants access");
        Console.WriteLine($"  3. Callback  → /callback?code={authCode}&state={state}");
        Console.WriteLine($"  4. Exchange  → POST {provider.TokenEndpoint}");
        Console.WriteLine($"     Response  → access_token: {accessToken[..16]}...");
        Console.WriteLine($"  5. UserInfo  → {{ id: {userId}, email: {email}, name: {name} }}");

        if (UserStore.TryGetValue(email, out var existing))
        {
            Console.WriteLine($"  6. Existing user — logged in as '{existing.Name}'");
        }
        else
        {
            UserStore[email] = new OAuthUser(userId, email, name, provider.Name, accessToken);
            Console.WriteLine($"  6. New user registered: {email} ({provider.Name})");
        }
        Console.WriteLine();
    }

    static string GenerateRandom(int bytes) =>
        Convert.ToBase64String(RandomNumberGenerator.GetBytes(bytes))
               .Replace("+", "").Replace("/", "").Replace("=", "")[..bytes];

    static string HashString(string input)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(hash).ToLower();
    }
}
