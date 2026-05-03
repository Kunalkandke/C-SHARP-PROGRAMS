// Program to recursively search all files of a specific extension in a directory
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string directoryPath = @"C:\Temp"; // Directory to search
        string extension = ".txt"; // File extension to search

        Console.WriteLine($"Searching for '{extension}' files in '{directoryPath}' and subdirectories:");
        SearchFiles(directoryPath, extension);
    }

    static void SearchFiles(string path, string extension)
    {
        try
        {
            foreach (var file in Directory.GetFiles(path, "*" + extension))
            {
                Console.WriteLine(file);
            }

            foreach (var dir in Directory.GetDirectories(path))
            {
                SearchFiles(dir, extension);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Access denied to: {path}");
        }
    }
}