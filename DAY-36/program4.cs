// Program to Compress/Decompress a file using GZipStream
using System;
using System.IO;
using System.IO.Compression;

class Program
{
    static void Main()
    {
        string inputFile = "example.txt";
        string compressedFile = "example.gz";
        string decompressedFile = "example_decompressed.txt";

        // Compress
        using (FileStream originalFileStream = new FileStream(inputFile, FileMode.Open))
        using (FileStream compressedFileStream = new FileStream(compressedFile, FileMode.Create))
        using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
        {
            originalFileStream.CopyTo(compressionStream);
        }
        Console.WriteLine($"File '{inputFile}' compressed to '{compressedFile}'");

        // Decompress
        using (FileStream compressedFileStream = new FileStream(compressedFile, FileMode.Open))
        using (FileStream decompressedFileStream = new FileStream(decompressedFile, FileMode.Create))
        using (GZipStream decompressionStream = new GZipStream(compressedFileStream, CompressionMode.Decompress))
        {
            decompressionStream.CopyTo(decompressedFileStream);
        }
        Console.WriteLine($"File '{compressedFile}' decompressed to '{decompressedFile}'");
    }
}