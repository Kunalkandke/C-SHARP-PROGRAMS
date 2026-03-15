// Dockerize .NET Application

using System;
using System.Collections.Generic;
using System.Text.Json;

record AppConfig(string AppName, string Environment, string Version);

class DockerizeNetApplication
{
    static readonly AppConfig Config = new("MyDotNetApp", "Production", "1.0.0");

    static void Main()
    {
        Console.WriteLine("=== Dockerize .NET Application ===\n");

        PrintDockerfile();
        PrintDockerCompose();
        PrintDockerCommands();
        SimulateContainerEnv();
    }

    static void PrintDockerfile()
    {
        Console.WriteLine("─── Dockerfile ───────────────────────────────────");
        Console.WriteLine(@"FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT [""dotnet"", ""MyDotNetApp.dll""]");
        Console.WriteLine();
    }

    static void PrintDockerCompose()
    {
        Console.WriteLine("─── docker-compose.yml ───────────────────────────");
        Console.WriteLine(@"version: '3.8'
services:
  api:
    build: .
    ports:
      - ""8080:8080""
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultDB=Server=db;Database=AppDB;User=sa;Password=Pass@123
    depends_on:
      - db
  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - SA_PASSWORD=Pass@123
      - ACCEPT_EULA=Y
    ports:
      - ""1433:1433""");
        Console.WriteLine();
    }

    static void PrintDockerCommands()
    {
        Console.WriteLine("─── Docker Commands ──────────────────────────────");
        var commands = new Dictionary<string, string>
        {
            ["Build image"]          = "docker build -t mydotnetapp:1.0 .",
            ["Run container"]        = "docker run -d -p 8080:8080 --name myapp mydotnetapp:1.0",
            ["List containers"]      = "docker ps",
            ["View logs"]            = "docker logs myapp",
            ["Stop container"]       = "docker stop myapp",
            ["Remove container"]     = "docker rm myapp",
            ["Remove image"]         = "docker rmi mydotnetapp:1.0",
            ["Compose up"]           = "docker-compose up -d",
            ["Compose down"]         = "docker-compose down",
            ["Push to Docker Hub"]   = "docker push username/mydotnetapp:1.0",
        };
        foreach (var (label, cmd) in commands)
            Console.WriteLine($"  {label,-24}: {cmd}");
        Console.WriteLine();
    }

    static void SimulateContainerEnv()
    {
        Console.WriteLine("─── Container Environment (simulated) ────────────");
        string env    = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        string port   = Environment.GetEnvironmentVariable("ASPNETCORE_URLS")        ?? "http://+:5000";
        Console.WriteLine($"  App Name   : {Config.AppName}");
        Console.WriteLine($"  Version    : {Config.Version}");
        Console.WriteLine($"  Environment: {env}");
        Console.WriteLine($"  URLs       : {port}");
        Console.WriteLine($"  Config JSON: {JsonSerializer.Serialize(Config)}");
    }
}
