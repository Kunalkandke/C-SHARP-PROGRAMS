// REST API with File Upload and Download

// ─── Testing with curl ────────────────────────────────────────────────────────
// Upload:   curl -X POST https://localhost:5001/upload -F "files=@report.pdf" -F "files=@photo.png"
// List:     curl https://localhost:5001/files
// Download: curl -OJ https://localhost:5001/download/report.pdf
// Delete:   curl -X DELETE https://localhost:5001/files/report.pdf

// Simulated demo — in-memory "file system" (no web server)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class FileEntry
{
    public string Name        { get; set; } = "";
    public string ContentType { get; set; } = "";
    public byte[] Data        { get; set; } = Array.Empty<byte>();
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public double SizeKB => Data.Length / 1024.0;
}

class RestApiFileUploadDownload
{
    static readonly Dictionary<string, FileEntry> Storage = new();

    static string GetContentType(string name) => Path.GetExtension(name).ToLower() switch
    {
        ".pdf"  => "application/pdf",
        ".png"  => "image/png",
        ".txt"  => "text/plain",
        ".csv"  => "text/csv",
        _       => "application/octet-stream"
    };

    // POST /upload
    static void Upload(string fileName, byte[] data)
    {
        string safe = Path.GetFileName(fileName);
        Storage[safe] = new FileEntry
        {
            Name        = safe,
            ContentType = GetContentType(safe),
            Data        = data,
            UploadedAt  = DateTime.UtcNow
        };
        Console.WriteLine($"POST /upload");
        Console.WriteLine($"  201 Created => {{ name: {safe}, type: {GetContentType(safe)}, sizeKB: {data.Length / 1024.0:F2} }}");
        Console.WriteLine();
    }

    // GET /files
    static void ListFiles()
    {
        Console.WriteLine("GET /files");
        if (Storage.Count == 0) { Console.WriteLine("  [] (empty)\n"); return; }
        foreach (var f in Storage.Values)
            Console.WriteLine($"  {{ name: {f.Name}, sizeKB: {f.SizeKB:F2}, type: {f.ContentType} }}");
        Console.WriteLine();
    }

    // GET /download/{fileName}
    static void Download(string fileName)
    {
        Console.WriteLine($"GET /download/{fileName}");
        if (!Storage.TryGetValue(fileName, out var file))
        {
            Console.WriteLine($"  404 Not Found\n"); return;
        }
        Console.WriteLine($"  200 OK  Content-Disposition: attachment; filename=\"{file.Name}\"");
        Console.WriteLine($"  Content-Type: {file.ContentType}  Bytes: {file.Data.Length}");
        Console.WriteLine($"  Preview: {Encoding.UTF8.GetString(file.Data[..Math.Min(60, file.Data.Length)])}...");
        Console.WriteLine();
    }

    // DELETE /files/{fileName}
    static void Delete(string fileName)
    {
        Console.WriteLine($"DELETE /files/{fileName}");
        if (!Storage.ContainsKey(fileName)) { Console.WriteLine("  404 Not Found\n"); return; }
        Storage.Remove(fileName);
        Console.WriteLine("  204 No Content\n");
    }

    static void Main()
    {
        Console.WriteLine("=== REST API with File Upload/Download (simulated) ===\n");

        // Simulate uploading files
        Upload("report.txt",   Encoding.UTF8.GetBytes(string.Concat(Enumerable.Repeat("This is a sample report line.\n", 10))));
        Upload("data.csv",     Encoding.UTF8.GetBytes("Id,Name,Price\n1,Laptop,75000\n2,Mouse,1500\n3,Keyboard,2500\n"));
        Upload("notes.txt",    Encoding.UTF8.GetBytes("Meeting notes: Discussed Q1 targets and budget allocation."));

        ListFiles();
        Download("data.csv");
        Download("missing.pdf");   // 404 demo
        Delete("notes.txt");
        ListFiles();
    }
}
