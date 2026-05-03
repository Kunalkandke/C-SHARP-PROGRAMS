// Program to read specific lines from a file using File.ReadLines (Memory Efficient)
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string filePath = "sample.txt"; // Input file path
        int startLine = 3;
        int endLine = 7;

        var specificLines = File.ReadLines(filePath)
                                .Skip(startLine - 1)
                                .Take(endLine - startLine + 1);

        Console.WriteLine($"Lines {startLine} to {endLine}:");
        foreach (var line in specificLines)
        {
            Console.WriteLine(line);
        }
    }
}