// Program to Split a Large File into smaller chunks based on line count
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string inputFile = "largefile.txt"; // Input file path
        int linesPerFile = 100; // Number of lines per chunk
        int fileIndex = 1;
        int lineCount = 0;

        StreamWriter writer = null;

        foreach (var line in File.ReadLines(inputFile))
        {
            if (lineCount % linesPerFile == 0)
            {
                writer?.Close();
                string chunkFileName = $"chunk_{fileIndex}.txt";
                writer = new StreamWriter(chunkFileName);
                fileIndex++;
            }

            writer.WriteLine(line);
            lineCount++;
        }

        writer?.Close();
        Console.WriteLine($"File split into {fileIndex - 1} chunks.");
    }
}
