// Configuration Management using appsettings.json

// Simulated demo (no NuGet required) — mimics reading from a config dictionary
using System;
using System.Collections.Generic;

class AppSettingsConfigManagerDemo
{
    // Simulated in-memory "appsettings.json" key-value store
    static readonly Dictionary<string, string> Config = new()
    {
        ["AppSettings:AppName"]    = "MyApp",
        ["AppSettings:Version"]    = "1.0.0",
        ["AppSettings:MaxRetries"] = "3",
        ["ConnectionStrings:DefaultDB"] = "Server=.;Database=TestDB;Integrated Security=True"
    };

    static string Get(string key) => Config.TryGetValue(key, out var v) ? v : "N/A";

    static void Main()
    {
        Console.WriteLine("=== Configuration Management (appsettings.json) ===\n");

        Console.WriteLine($"App Name   : {Get("AppSettings:AppName")}");
        Console.WriteLine($"Version    : {Get("AppSettings:Version")}");
        Console.WriteLine($"Max Retries: {Get("AppSettings:MaxRetries")}");
        Console.WriteLine($"Connection : {Get("ConnectionStrings:DefaultDB")}");

        Console.WriteLine("\n[Bound to AppSettings object]");
        var settings = new AppSettings
        {
            AppName    = Get("AppSettings:AppName"),
            Version    = Get("AppSettings:Version"),
            MaxRetries = int.Parse(Get("AppSettings:MaxRetries"))
        };
        Console.WriteLine($"Name: {settings.AppName}, Version: {settings.Version}, MaxRetries: {settings.MaxRetries}");
    }
}

class AppSettings
{
    public string AppName    { get; set; } = "";
    public string Version    { get; set; } = "";
    public int    MaxRetries { get; set; }
}
