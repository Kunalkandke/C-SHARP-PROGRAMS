// API Consumption using HttpClient
// No extra NuGet packages needed — HttpClient is built into .NET
// Uses: https://jsonplaceholder.typicode.com (free public mock REST API)

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

// ─── DTOs ─────────────────────────────────────────────────────────────────────
class Post
{
    [JsonPropertyName("userId")]  public int    UserId { get; set; }
    [JsonPropertyName("id")]      public int    Id     { get; set; }
    [JsonPropertyName("title")]   public string Title  { get; set; } = "";
    [JsonPropertyName("body")]    public string Body   { get; set; } = "";
}

class HttpClientApiConsumer
{
    private static readonly HttpClient client = new HttpClient
    {
        BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
    };

    static async Task Main()
    {
        Console.WriteLine("=== API Consumption using HttpClient ===\n");

        await GetAllPostsAsync();
        await GetSinglePostAsync(1);
        await CreatePostAsync();
        await UpdatePostAsync(1);
        await DeletePostAsync(1);
    }

    // GET  /posts — fetch all posts (show first 3)
    static async Task GetAllPostsAsync()
    {
        Console.WriteLine("--- GET /posts (first 3) ---");
        var posts = await client.GetFromJsonAsync<List<Post>>("posts");
        foreach (var p in posts!.GetRange(0, 3))
            Console.WriteLine($"  [{p.Id}] {p.Title}");
        Console.WriteLine();
    }

    // GET  /posts/1 — fetch single post
    static async Task GetSinglePostAsync(int id)
    {
        Console.WriteLine($"--- GET /posts/{id} ---");
        var post = await client.GetFromJsonAsync<Post>($"posts/{id}");
        Console.WriteLine($"  Title : {post!.Title}");
        Console.WriteLine($"  Body  : {post.Body[..50]}...");
        Console.WriteLine();
    }

    // POST /posts — create new post
    static async Task CreatePostAsync()
    {
        Console.WriteLine("--- POST /posts ---");
        var newPost = new Post { UserId = 1, Title = "New Post", Body = "Post body content." };
        var response = await client.PostAsJsonAsync("posts", newPost);
        var created  = await response.Content.ReadFromJsonAsync<Post>();
        Console.WriteLine($"  Created post with Id: {created!.Id}, Title: {created.Title}");
        Console.WriteLine();
    }

    // PUT  /posts/1 — update post
    static async Task UpdatePostAsync(int id)
    {
        Console.WriteLine($"--- PUT /posts/{id} ---");
        var updated = new Post { Id = id, UserId = 1, Title = "Updated Title", Body = "Updated body." };
        var response = await client.PutAsJsonAsync($"posts/{id}", updated);
        var result   = await response.Content.ReadFromJsonAsync<Post>();
        Console.WriteLine($"  Updated Title: {result!.Title}");
        Console.WriteLine();
    }

    // DELETE /posts/1
    static async Task DeletePostAsync(int id)
    {
        Console.WriteLine($"--- DELETE /posts/{id} ---");
        var response = await client.DeleteAsync($"posts/{id}");
        Console.WriteLine($"  Status: {(int)response.StatusCode} {response.StatusCode}");
        Console.WriteLine();
    }
}
