// Program to merge contents of multiple log files into one
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string[] logFiles = { "log1.txt", "log2.txt", "log3.txt" }; // Input log files
        string mergedFile = "mergedLog.txt";

        using (StreamWriter writer = new StreamWriter(mergedFile))
        {
            foreach (var file in logFiles)
            {
                if (File.Exists(file))
                {
                    foreach (var line in File.ReadLines(file))
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }

        Console.WriteLine($"Merged {logFiles.Length} log files into {mergedFile}");
    }
}