// Azure Blob Storage Integration

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

class BlobFile
{
    public string   Name        { get; set; } = "";
    public string   ContentType { get; set; } = "";
    public byte[]   Data        { get; set; } = Array.Empty<byte>();
    public long     SizeBytes   => Data.Length;
    public DateTime UploadedAt  { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = new();
}

class BlobContainer
{
    public string Name  { get; }
    readonly Dictionary<string, BlobFile> _blobs = new();

    public BlobContainer(string name) { Name = name; }

    public void Upload(string blobName, byte[] data, string contentType, Dictionary<string, string>? metadata = null)
    {
        _blobs[blobName] = new BlobFile
        {
            Name        = blobName,
            ContentType = contentType,
            Data        = data,
            UploadedAt  = DateTime.UtcNow,
            Metadata    = metadata ?? new()
        };
    }

    public BlobFile? Download(string blobName) =>
        _blobs.TryGetValue(blobName, out var b) ? b : null;

    public bool Delete(string blobName)   => _blobs.Remove(blobName);
    public bool Exists(string blobName)   => _blobs.ContainsKey(blobName);
    public IEnumerable<BlobFile> List()   => _blobs.Values;
    public string GetUrl(string blobName) => $"https://mystorageaccount.blob.core.windows.net/{Name}/{blobName}";
}

class AzureBlobStorageIntegration
{
    static void Main()
    {
        Console.WriteLine("=== Azure Blob Storage Integration ===\n");

        PrintSdkSetup();

        var container = new BlobContainer("uploads");
        Console.WriteLine($"Container '{container.Name}' created.\n");

        Upload(container, "report.txt",    "Monthly sales report data.\nTotal: Rs.1,20,000", "text/plain");
        Upload(container, "config.json",   JsonSerializer.Serialize(new { env = "prod", version = "1.0" }), "application/json");
        Upload(container, "photo.png",     "PNG_BINARY_DATA_SIMULATED", "image/png",
               new Dictionary<string, string> { ["author"] = "Alice", ["category"] = "profile" });

        ListBlobs(container);
        DownloadBlob(container, "config.json");
        DownloadBlob(container, "missing.pdf");
        GetBlobUrl(container, "report.txt");
        DeleteBlob(container, "photo.png");
        ListBlobs(container);
    }

    static void PrintSdkSetup()
    {
        Console.WriteLine("─── SDK Setup ────────────────────────────────────");
        Console.WriteLine("  dotnet add package Azure.Storage.Blobs\n");
        Console.WriteLine(@"  var client    = new BlobServiceClient(connectionString);
  var container = client.GetBlobContainerClient(""uploads"");
  await container.CreateIfNotExistsAsync(PublicAccessType.None);
  // Upload:   await container.GetBlobClient(name).UploadAsync(stream);
  // Download: await container.GetBlobClient(name).DownloadToAsync(stream);
  // Delete:   await container.GetBlobClient(name).DeleteIfExistsAsync();
  // List:     await foreach (var blob in container.GetBlobsAsync()) { ... }
");
    }

    static void Upload(BlobContainer c, string name, string text, string ct, Dictionary<string, string>? meta = null)
    {
        byte[] data = Encoding.UTF8.GetBytes(text);
        c.Upload(name, data, ct, meta);
        Console.WriteLine($"UPLOAD  '{name}'  [{ct}]  {data.Length} bytes");
        if (meta?.Count > 0) Console.WriteLine($"  Metadata: {JsonSerializer.Serialize(meta)}");
    }

    static void ListBlobs(BlobContainer c)
    {
        Console.WriteLine($"\nLIST blobs in '{c.Name}':");
        foreach (var b in c.List())
            Console.WriteLine($"  {b.Name,-20} {b.ContentType,-22} {b.SizeBytes,5} bytes  {b.UploadedAt:HH:mm:ss}");
        Console.WriteLine();
    }

    static void DownloadBlob(BlobContainer c, string name)
    {
        var blob = c.Download(name);
        Console.WriteLine($"DOWNLOAD '{name}':");
        if (blob is null) { Console.WriteLine("  404 Blob not found.\n"); return; }
        Console.WriteLine($"  Content-Type: {blob.ContentType}");
        Console.WriteLine($"  Content     : {Encoding.UTF8.GetString(blob.Data[..Math.Min(60, blob.Data.Length)])}");
        Console.WriteLine();
    }

    static void GetBlobUrl(BlobContainer c, string name)
    {
        Console.WriteLine($"URL '{name}':");
        Console.WriteLine($"  {c.GetUrl(name)}\n");
    }

    static void DeleteBlob(BlobContainer c, string name)
    {
        bool removed = c.Delete(name);
        Console.WriteLine($"DELETE '{name}': {(removed ? "204 No Content" : "404 Not Found")}\n");
    }
}
