// Secure Password Hashing

using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

class UserAccount
{
    public int    Id           { get; set; }
    public string Username     { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string Salt         { get; set; } = "";
    public string Algorithm    { get; set; } = "";
}

class PasswordHasher
{
    const int SaltSize   = 32;
    const int HashSize   = 32;
    const int Iterations = 600_000;

    public (string Hash, string Salt) Hash(string password)
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(SaltSize);

        byte[] hashBytes = Rfc2898DeriveBytes.Pbkdf2(
            password:        Encoding.UTF8.GetBytes(password),
            salt:            saltBytes,
            iterations:      Iterations,
            hashAlgorithm:   HashAlgorithmName.SHA256,
            outputLength:    HashSize);

        return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
    }

    public bool Verify(string password, string storedHash, string storedSalt)
    {
        byte[] saltBytes   = Convert.FromBase64String(storedSalt);
        byte[] storedBytes = Convert.FromBase64String(storedHash);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
            password:      Encoding.UTF8.GetBytes(password),
            salt:          saltBytes,
            iterations:    Iterations,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength:  HashSize);

        return CryptographicOperations.FixedTimeEquals(inputHash, storedBytes);
    }
}

class SecurePasswordHashing
{
    static readonly PasswordHasher Hasher = new();
    static readonly List<UserAccount> Users = new();
    static int _nextId = 1;

    static void Main()
    {
        Console.WriteLine("=== Secure Password Hashing ===\n");

        PrintWhyNotMD5SHA1();
        PrintAlgorithmComparison();

        Console.WriteLine("─── Registration & Login Demo ────────────────────\n");

        Register("alice",   "MyP@ssw0rd!123");
        Register("bob",     "SecureB0b$2024");
        Register("charlie", "Ch@rl!ePass99");

        Console.WriteLine();
        Login("alice",   "MyP@ssw0rd!123");
        Login("alice",   "wrongpassword");
        Login("bob",     "SecureB0b$2024");
        Login("charlie", "Ch@rl!ePass99");
        Login("nobody",  "anything");

        Console.WriteLine("\n─── Stored Hash Details (alice) ──────────────────");
        var user = Users.Find(u => u.Username == "alice")!;
        Console.WriteLine($"  Algorithm  : {user.Algorithm}");
        Console.WriteLine($"  Salt       : {user.Salt[..20]}...");
        Console.WriteLine($"  Hash       : {user.PasswordHash[..20]}...");
        Console.WriteLine($"  Same input, different hash each time: {DemoNonDeterminism()}");
    }

    static void Register(string username, string password)
    {
        var (hash, salt) = Hasher.Hash(password);
        Users.Add(new UserAccount
        {
            Id           = _nextId++,
            Username     = username,
            PasswordHash = hash,
            Salt         = salt,
            Algorithm    = "PBKDF2-HMAC-SHA256 (600k iterations)"
        });
        Console.WriteLine($"  [REGISTER] {username,-10} => hash stored (password never saved)");
    }

    static void Login(string username, string password)
    {
        var user = Users.Find(u => u.Username == username);
        if (user is null)
        {
            Console.WriteLine($"  [LOGIN   ] {username,-10} => 401 User not found.");
            return;
        }
        bool ok = Hasher.Verify(password, user.PasswordHash, user.Salt);
        Console.WriteLine($"  [LOGIN   ] {username,-10} => {(ok ? "200 OK — Authenticated" : "401 Unauthorized — Wrong password")}");
    }

    static string DemoNonDeterminism()
    {
        var (h1, _) = Hasher.Hash("samepassword");
        var (h2, _) = Hasher.Hash("samepassword");
        return h1 != h2 ? "YES (random salt per hash)" : "NO";
    }

    static void PrintWhyNotMD5SHA1()
    {
        Console.WriteLine("─── Why NOT MD5 / SHA1 / SHA256 directly? ────────");
        Console.WriteLine("  MD5 / SHA1   : Fast → easily brute-forced, rainbow tables exist");
        Console.WriteLine("  SHA256 plain : Still fast, no salting by default");
        Console.WriteLine("  PBKDF2       : Slow (600k iterations), salted, collision-resistant");
        Console.WriteLine("  BCrypt       : Adaptive cost factor, widely used");
        Console.WriteLine("  Argon2id     : Winner of Password Hashing Competition (recommended)\n");
    }

    static void PrintAlgorithmComparison()
    {
        Console.WriteLine("─── Algorithm Comparison ─────────────────────────");
        Console.WriteLine($"  {"Algorithm",-20} {"Speed",-12} {"Salted",-10} {".NET Built-in"}");
        Console.WriteLine($"  {"MD5",-20} {"~3 ns",-12} {"No",-10} Yes");
        Console.WriteLine($"  {"SHA256",-20} {"~5 ns",-12} {"No",-10} Yes");
        Console.WriteLine($"  {"PBKDF2-SHA256",-20} {"~200 ms",-12} {"Yes",-10} Yes (Rfc2898)");
        Console.WriteLine($"  {"BCrypt",-20} {"~250 ms",-12} {"Yes",-10} NuGet: BCrypt.Net");
        Console.WriteLine($"  {"Argon2id",-20} {"~300 ms",-12} {"Yes",-10} NuGet: Konscious.Security");
        Console.WriteLine();
    }
}
