// CI/CD Pipeline for .NET using GitHub Actions

using System;
using System.Collections.Generic;

class CICDPipelineGitHubActions
{
    static void Main()
    {
        Console.WriteLine("=== CI/CD Pipeline for .NET using GitHub Actions ===\n");

        PrintWorkflowYaml();
        SimulatePipelineRun();
    }

    static void PrintWorkflowYaml()
    {
        Console.WriteLine("─── .github/workflows/dotnet-cicd.yml ────────────");
        Console.WriteLine(@"name: .NET CI/CD Pipeline

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

env:
  DOTNET_VERSION: '10.0.x'
  AZURE_WEBAPP_NAME: my-dotnet-app

jobs:
  build-and-test:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout code
        uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: ${{ env.DOTNET_VERSION }}

      - name: Restore dependencies
        run: dotnet restore

      - name: Build
        run: dotnet build --no-restore -c Release

      - name: Run unit tests
        run: dotnet test --no-build -c Release --verbosity normal \
               --collect:'XPlat Code Coverage'

      - name: Upload coverage report
        uses: codecov/codecov-action@v4
        with:
          token: ${{ secrets.CODECOV_TOKEN }}

  publish-and-deploy:
    runs-on: ubuntu-latest
    needs: build-and-test
    if: github.ref == 'refs/heads/main'
    steps:
      - uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: ${{ env.DOTNET_VERSION }}

      - name: Publish
        run: dotnet publish -c Release -o ./publish

      - name: Deploy to Azure Web App
        uses: azure/webapps-deploy@v3
        with:
          app-name: ${{ env.AZURE_WEBAPP_NAME }}
          publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
          package: ./publish");
        Console.WriteLine();
    }

    static void SimulatePipelineRun()
    {
        Console.WriteLine("─── Simulated Pipeline Run ───────────────────────");

        var steps = new List<(string Name, bool Success, string Output)>
        {
            ("Checkout code",           true,  "Cloned repo @ main (a1b2c3d)"),
            ("Setup .NET 10.0",         true,  ".NET SDK 10.0.100 installed"),
            ("Restore dependencies",    true,  "Restored 12 packages in 3.4s"),
            ("Build (Release)",         true,  "Build succeeded, 0 warnings"),
            ("Run unit tests",          true,  "Passed: 24 | Failed: 0 | Skipped: 0"),
            ("Upload coverage report",  true,  "Coverage: 87.4% uploaded to Codecov"),
            ("Publish",                 true,  "Published to ./publish (15 files)"),
            ("Deploy to Azure",         true,  "Deployed to https://my-dotnet-app.azurewebsites.net"),
        };

        foreach (var (name, success, output) in steps)
        {
            string icon = success ? "✓" : "✗";
            Console.WriteLine($"  [{icon}] {name,-30} => {output}");
        }

        Console.WriteLine("\n  Pipeline completed successfully. Deployment live!");
    }
}
