// Real-time Communication using SignalR

// Simulated demo — in-process hub with multiple virtual clients (no web server)
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class SimulatedHub
{
    public delegate Task MessageHandler(string user, string message);

    // All connected client handlers
    readonly List<(string Id, MessageHandler Handler)> _clients = new();
    readonly Dictionary<string, List<string>> _groups = new();

    public string Connect(MessageHandler handler)
    {
        string id = $"conn-{_clients.Count + 1}";
        _clients.Add((id, handler));
        _ = BroadcastExcept(id, "System", $"{id} connected.");
        return id;
    }

    public async Task Disconnect(string connId)
    {
        _clients.RemoveAll(c => c.Id == connId);
        await BroadcastExcept(connId, "System", $"{connId} disconnected.");
    }

    public async Task SendAll(string user, string message)
    {
        foreach (var c in _clients)
            await c.Handler(user, message);
    }

    public async Task JoinGroup(string connId, string group)
    {
        if (!_groups.ContainsKey(group)) _groups[group] = new();
        _groups[group].Add(connId);
        await SendToGroup(group, "System", $"{connId} joined '{group}'.");
    }

    public async Task SendToGroup(string group, string user, string msg)
    {
        if (!_groups.TryGetValue(group, out var members)) return;
        foreach (var id in members)
        {
            var client = _clients.Find(c => c.Id == id);
            if (client != default) await client.Handler(user, msg);
        }
    }

    async Task BroadcastExcept(string excludeId, string user, string msg)
    {
        foreach (var c in _clients)
            if (c.Id != excludeId) await c.Handler(user, msg);
    }
}

class SignalRRealTimeCommunication
{
    static async Task Main()
    {
        Console.WriteLine("=== Real-time Communication using SignalR (simulated) ===\n");

        var hub = new SimulatedHub();

        // Connect 3 clients
        string alice = hub.Connect((u, m) => { Console.WriteLine($"  [Alice   ] {u}: {m}"); return Task.CompletedTask; });
        string bob   = hub.Connect((u, m) => { Console.WriteLine($"  [Bob     ] {u}: {m}"); return Task.CompletedTask; });
        string charlie = hub.Connect((u, m) => { Console.WriteLine($"  [Charlie ] {u}: {m}"); return Task.CompletedTask; });

        Console.WriteLine("\n--- Broadcast to All ---");
        await hub.SendAll("Alice", "Hello everyone!");

        Console.WriteLine("\n--- Join Group 'Room1' (Alice, Bob) ---");
        await hub.JoinGroup(alice, "Room1");
        await hub.JoinGroup(bob, "Room1");

        Console.WriteLine("\n--- Send to Group 'Room1' ---");
        await hub.SendToGroup("Room1", "Alice", "Hey Room1, how are you?");

        Console.WriteLine("\n--- Charlie disconnects ---");
        await hub.Disconnect(charlie);

        Console.WriteLine("\n--- Broadcast after Charlie left ---");
        await hub.SendAll("Bob", "Goodbye Charlie!");
    }
}
