// Program to monitor a directory for changes using FileSystemWatcher
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = @"C:\Temp"; // Set your directory path

        using (FileSystemWatcher watcher = new FileSystemWatcher())
        {
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
            watcher.Filter = "*.*";

            watcher.Created += OnChanged;
            watcher.Changed += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;

            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Monitoring changes in {path}. Press Enter to exit.");
            Console.ReadLine();
        }
    }

    private static void OnChanged(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"File {e.ChangeType}: {e.FullPath}");
    }

    private static void OnRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"File Renamed: {e.OldFullPath} -> {e.FullPath}");
    }
}