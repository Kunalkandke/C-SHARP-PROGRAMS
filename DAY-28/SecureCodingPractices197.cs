// Secure Coding Practices in C#

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

class SecureCodingPractices
{
    static void Main()
    {
        Console.WriteLine("=== Secure Coding Practices in C# ===\n");

        Demo_InputValidation();
        Demo_SqlInjectionPrevention();
        Demo_XssPrevention();
        Demo_SecureRandomness();
        Demo_SensitiveDataHandling();
        Demo_PathTraversalPrevention();
        Demo_SecureHeaders();
        PrintSecurityChecklist();
    }

    // 1. Input Validation ───────────────────────────────────────────────────
    static void Demo_InputValidation()
    {
        Console.WriteLine("─── 1. Input Validation ──────────────────────────");

        string[] emails = { "user@example.com", "not-an-email", "admin@site.org", "'; DROP TABLE--" };
        foreach (var e in emails)
        {
            bool valid = Regex.IsMatch(e, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            Console.WriteLine($"  {e,-30} => {(valid ? "Valid" : "Invalid — rejected")}");
        }

        int[] ages = { -1, 0, 17, 25, 150, 200 };
        foreach (var age in ages)
        {
            bool valid = age is >= 18 and <= 120;
            Console.WriteLine($"  Age {age,-5} => {(valid ? "Valid" : "Invalid — rejected")}");
        }
        Console.WriteLine();
    }

    // 2. SQL Injection Prevention ───────────────────────────────────────────
    static void Demo_SqlInjectionPrevention()
    {
        Console.WriteLine("─── 2. SQL Injection Prevention ──────────────────");
        string malicious = "' OR '1'='1'; DROP TABLE Users; --";

        Console.WriteLine($"  Input       : {malicious}");
        Console.WriteLine($"  UNSAFE query: SELECT * FROM Users WHERE Name = '{malicious}'");
        Console.WriteLine($"                ⚠ This allows injection!");
        Console.WriteLine();
        Console.WriteLine("  SAFE (parameterized):");
        Console.WriteLine(@"    // SqlCommand
    cmd.CommandText = ""SELECT * FROM Users WHERE Name = @Name"";
    cmd.Parameters.AddWithValue(""@Name"", userInput);
    // EF Core (always parameterized):
    db.Users.Where(u => u.Name == userInput).ToList();");
        Console.WriteLine();
    }

    // 3. XSS Prevention ────────────────────────────────────────────────────
    static void Demo_XssPrevention()
    {
        Console.WriteLine("─── 3. XSS Prevention ────────────────────────────");
        string[] inputs = { "Hello World", "<script>alert('XSS')</script>", "<img src=x onerror=alert(1)>", "Normal text & symbols <>" };

        foreach (var input in inputs)
        {
            string encoded = System.Web.HttpUtility.HtmlEncode(input);
            Console.WriteLine($"  Raw    : {input}");
            Console.WriteLine($"  Encoded: {encoded}\n");
        }
    }

    // 4. Cryptographically Secure Randomness ───────────────────────────────
    static void Demo_SecureRandomness()
    {
        Console.WriteLine("─── 4. Secure Random Number Generation ───────────");
        Console.WriteLine("  // BAD: System.Random is not cryptographically secure");
        Console.WriteLine("  // var token = new Random().Next();  ← predictable!\n");

        byte[] tokenBytes = RandomNumberGenerator.GetBytes(32);
        string token = Convert.ToBase64String(tokenBytes);
        Console.WriteLine($"  Secure token (256-bit): {token}");

        int otp = RandomNumberGenerator.GetInt32(100000, 999999);
        Console.WriteLine($"  Secure OTP (6-digit)  : {otp}");
        Console.WriteLine();
    }

    // 5. Sensitive Data Handling ───────────────────────────────────────────
    static void Demo_SensitiveDataHandling()
    {
        Console.WriteLine("─── 5. Sensitive Data Handling ───────────────────");

        string password = "MyP@ssword123!";
        byte[] salt     = RandomNumberGenerator.GetBytes(32);
        byte[] hash     = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password), salt, 600_000, HashAlgorithmName.SHA256, 32);

        Console.WriteLine($"  Password in code : *** (never hardcode passwords)");
        Console.WriteLine($"  Password hash    : {Convert.ToBase64String(hash)[..20]}...");
        Console.WriteLine($"  Salt             : {Convert.ToBase64String(salt)[..20]}...");
        Console.WriteLine();
        Console.WriteLine("  Best practices:");
        Console.WriteLine("  • Store secrets in Azure Key Vault / env vars, NOT in code");
        Console.WriteLine("  • Use PBKDF2/BCrypt/Argon2id for passwords");
        Console.WriteLine("  • Log request IDs, never log passwords/tokens/PII");
        Console.WriteLine("  • Encrypt PII at rest (AES-256-GCM)");
        Console.WriteLine();
    }

    // 6. Path Traversal Prevention ─────────────────────────────────────────
    static void Demo_PathTraversalPrevention()
    {
        Console.WriteLine("─── 6. Path Traversal Prevention ─────────────────");
        string baseDir = "/var/app/uploads";
        string[] requests = { "report.pdf", "../../etc/passwd", "../secret.txt", "docs/manual.pdf" };

        foreach (var req in requests)
        {
            string safeName = System.IO.Path.GetFileName(req);
            string fullPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDir, safeName));
            bool   safe     = fullPath.StartsWith(baseDir, StringComparison.OrdinalIgnoreCase);
            Console.WriteLine($"  Input: {req,-25} Safe name: {safeName,-15} => {(safe ? "Allowed" : "BLOCKED — path traversal")}");
        }
        Console.WriteLine();
    }

    // 7. Secure HTTP Headers ───────────────────────────────────────────────
    static void Demo_SecureHeaders()
    {
        Console.WriteLine("─── 7. Secure HTTP Headers ───────────────────────");
        var headers = new Dictionary<string, string>
        {
            ["Strict-Transport-Security"]  = "max-age=31536000; includeSubDomains",
            ["X-Content-Type-Options"]     = "nosniff",
            ["X-Frame-Options"]            = "DENY",
            ["Content-Security-Policy"]    = "default-src 'self'; script-src 'self'",
            ["Referrer-Policy"]            = "strict-origin-when-cross-origin",
            ["Permissions-Policy"]         = "camera=(), microphone=(), geolocation=()",
        };
        foreach (var (k, v) in headers) Console.WriteLine($"  {k,-35}: {v}");
        Console.WriteLine();
    }

    static void PrintSecurityChecklist()
    {
        Console.WriteLine("─── Security Checklist ───────────────────────────");
        string[] checks =
        {
            "Validate and sanitize all user input",
            "Use parameterized queries / ORM (never string concat SQL)",
            "Hash passwords with PBKDF2 / BCrypt / Argon2id",
            "Generate tokens with RandomNumberGenerator, not System.Random",
            "Store secrets in Key Vault / env vars, never in source code",
            "Enable HTTPS and HSTS",
            "Set security headers (CSP, X-Frame-Options, etc.)",
            "Implement rate limiting on auth endpoints",
            "Use anti-forgery tokens on state-changing requests",
            "Sanitize file names; restrict upload types and sizes",
            "Log security events; never log passwords, tokens, or PII",
            "Keep NuGet packages up-to-date (run: dotnet list package --vulnerable)",
        };
        foreach (var c in checks) Console.WriteLine($"  [✓] {c}");
    }
}
