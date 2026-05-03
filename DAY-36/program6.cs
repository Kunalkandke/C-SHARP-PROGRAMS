// Program to copy a file with Percentage Progress tracking
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string sourceFile = "largefile.txt";
        string destinationFile = "copy_largefile.txt";
        const int bufferSize = 81920; // 80 KB

        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
        using (FileStream destStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write))
        {
            long totalBytes = sourceStream.Length;
            long totalRead = 0;
            byte[] buffer = new byte[bufferSize];
            int bytesRead;

            while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                destStream.Write(buffer, 0, bytesRead);
                totalRead += bytesRead;
                double percent = (double)totalRead / totalBytes * 100;
                Console.Write($"\rProgress: {percent:F2}%");
            }
        }

        Console.WriteLine("\nFile copy completed.");
    }
}