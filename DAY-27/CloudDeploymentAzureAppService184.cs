// Cloud Deployment on Azure App Service

using System;
using System.Collections.Generic;

class CloudDeploymentAzureAppService
{
    static void Main()
    {
        Console.WriteLine("=== Cloud Deployment on Azure App Service ===\n");

        PrintAzureCliCommands();
        PrintAppServiceConfig();
        SimulateDeployment();
        PrintPostDeployChecklist();
    }

    static void PrintAzureCliCommands()
    {
        Console.WriteLine("─── Azure CLI Deployment Steps ───────────────────");
        var steps = new[]
        {
            ("Login to Azure",              "az login"),
            ("Create resource group",       "az group create --name MyRG --location eastus"),
            ("Create App Service plan",     "az appservice plan create --name MyPlan --resource-group MyRG --sku B1 --is-linux"),
            ("Create Web App",              "az webapp create --name my-dotnet-app --resource-group MyRG --plan MyPlan --runtime 'DOTNETCORE:10.0'"),
            ("Publish app",                 "dotnet publish -c Release -o ./publish"),
            ("Zip output",                  "cd publish && zip -r ../app.zip ."),
            ("Deploy zip",                  "az webapp deploy --resource-group MyRG --name my-dotnet-app --src-path app.zip --type zip"),
            ("Set env variable",            "az webapp config appsettings set --name my-dotnet-app --resource-group MyRG --settings ASPNETCORE_ENVIRONMENT=Production"),
            ("Enable logging",              "az webapp log config --name my-dotnet-app --resource-group MyRG --application-logging filesystem --level information"),
            ("Stream live logs",            "az webapp log tail --name my-dotnet-app --resource-group MyRG"),
        };

        foreach (var (desc, cmd) in steps)
            Console.WriteLine($"  # {desc}\n  {cmd}\n");
    }

    static void PrintAppServiceConfig()
    {
        Console.WriteLine("─── App Service Configuration ────────────────────");
        Console.WriteLine(@"  Tier           : Basic B1 (1 core, 1.75 GB RAM)
  Runtime        : .NET 10.0 (Linux)
  Region         : East US
  URL            : https://my-dotnet-app.azurewebsites.net
  Custom Domain  : myapp.example.com  (CNAME -> my-dotnet-app.azurewebsites.net)
  SSL            : Managed certificate (free via App Service)
  Scale Out      : Manual (1-10 instances) or Auto-scale rule
  Deployment Slot: staging -> swap to production (zero-downtime)");
        Console.WriteLine();
    }

    static void SimulateDeployment()
    {
        Console.WriteLine("─── Deployment Simulation ────────────────────────");
        var stages = new List<(string Stage, string Detail)>
        {
            ("Build",      "dotnet publish -c Release → ./publish (24 files, 8.3 MB)"),
            ("Package",    "zip ./publish → app.zip (3.1 MB compressed)"),
            ("Upload",     "Uploading app.zip to Azure... 100%"),
            ("Deploy",     "Extracting and deploying to /home/site/wwwroot"),
            ("Restart",    "App Service restarted with new build"),
            ("Health",     "GET /health → 200 OK (startup: 2.1s)"),
            ("Live",       "https://my-dotnet-app.azurewebsites.net is responding"),
        };

        foreach (var (stage, detail) in stages)
            Console.WriteLine($"  [✓] {stage,-10}: {detail}");
        Console.WriteLine();
    }

    static void PrintPostDeployChecklist()
    {
        Console.WriteLine("─── Post-Deployment Checklist ────────────────────");
        string[] checks =
        {
            "Health endpoint returns 200",
            "Connection strings set in App Settings (not appsettings.json)",
            "HTTPS redirect enabled",
            "Custom domain + SSL certificate bound",
            "Application Insights attached for monitoring",
            "Auto-scale rule configured",
            "Deployment slot (staging) set up for zero-downtime deploys",
            "Backup policy enabled",
        };
        foreach (var c in checks)
            Console.WriteLine($"  [✓] {c}");
    }
}
