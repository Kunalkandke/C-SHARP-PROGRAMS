// Azure Key Vault Integration

using System;
using System.Collections.Generic;

class SecretEntry
{
    public string Value     { get; set; } = "";
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public bool     Enabled { get; set; } = true;
}

class KeyVaultClient
{
    readonly Dictionary<string, SecretEntry> _secrets = new();

    public void SetSecret(string name, string value) =>
        _secrets[name] = new SecretEntry { Value = value };

    public string? GetSecret(string name) =>
        _secrets.TryGetValue(name, out var s) && s.Enabled ? s.Value : null;

    public void DeleteSecret(string name)
    {
        if (_secrets.TryGetValue(name, out var s)) s.Enabled = false;
    }

    public IEnumerable<string> ListSecrets()
    {
        foreach (var kv in _secrets)
            if (kv.Value.Enabled) yield return kv.Key;
    }
}

class AzureKeyVaultIntegration
{
    static void Main()
    {
        Console.WriteLine("=== Azure Key Vault Integration ===\n");

        PrintSdkSetup();
        PrintProgramCsSetup();

        var vault = new KeyVaultClient();
        Console.WriteLine("─── Secret Operations (simulated) ────────────────\n");

        SetSecret(vault, "ConnectionStrings--DefaultDB", "Server=tcp:myserver.database.windows.net;Database=AppDB;User=admin;Password=Pass@123;");
        SetSecret(vault, "Jwt--SecretKey",               "MySuperSecretJwtKey_32CharsMinimum!");
        SetSecret(vault, "Storage--ConnectionString",    "DefaultEndpointsProtocol=https;AccountName=mystorage;AccountKey=ABC123==");
        SetSecret(vault, "EmailSender--SmtpPassword",    "smtp_pass_XyZ789");

        Console.WriteLine("\nLIST secrets in vault:");
        foreach (var name in vault.ListSecrets())
            Console.WriteLine($"  {name}");

        Console.WriteLine();
        GetSecret(vault, "Jwt--SecretKey");
        GetSecret(vault, "NonExistent--Key");

        Console.WriteLine("\nDELETE 'EmailSender--SmtpPassword':");
        vault.DeleteSecret("EmailSender--SmtpPassword");
        Console.WriteLine("  Secret disabled (soft-delete). Purge in 90 days.");

        Console.WriteLine("\nLIST after delete:");
        foreach (var name in vault.ListSecrets())
            Console.WriteLine($"  {name}");
    }

    static void PrintSdkSetup()
    {
        Console.WriteLine("─── NuGet Packages ───────────────────────────────");
        Console.WriteLine("  dotnet add package Azure.Security.KeyVault.Secrets");
        Console.WriteLine("  dotnet add package Azure.Identity\n");
    }

    static void PrintProgramCsSetup()
    {
        Console.WriteLine("─── Program.cs Integration ───────────────────────");
        Console.WriteLine(@"  // Use Managed Identity (no credentials in code)
  builder.Configuration.AddAzureKeyVault(
      new Uri(""https://mykeyvault.vault.azure.net/""),
      new DefaultAzureCredential());

  // Access secret anywhere via IConfiguration:
  string dbConn = builder.Configuration[""ConnectionStrings--DefaultDB""];

  // Or directly:
  var client = new SecretClient(
      new Uri(""https://mykeyvault.vault.azure.net/""),
      new DefaultAzureCredential());
  KeyVaultSecret secret = await client.GetSecretAsync(""Jwt--SecretKey"");
  string jwtKey = secret.Value;
");
    }

    static void SetSecret(KeyVaultClient vault, string name, string value)
    {
        vault.SetSecret(name, value);
        string masked = new string('*', Math.Min(value.Length, 8)) + "..." ;
        Console.WriteLine($"SET '{name}' => {masked}");
    }

    static void GetSecret(KeyVaultClient vault, string name)
    {
        string? val = vault.GetSecret(name);
        if (val is null) Console.WriteLine($"GET '{name}' => 404 Secret not found.");
        else
        {
            string masked = val[..Math.Min(12, val.Length)] + "...";
            Console.WriteLine($"GET '{name}' => {masked}");
        }
    }
}
