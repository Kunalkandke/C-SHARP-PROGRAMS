using System;
using System.IO;

class FileHandlingDemo
{
    static void Main()
    {
        string filePath = "sample.txt";

        // ---------- Writing to a file ----------
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Hello, this is a sample text file.");
                writer.WriteLine("This file is created using StreamWriter.");
                writer.WriteLine("File handling in C# is easy to understand.");
            }
            Console.WriteLine("Data written to file successfully.\n");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while writing to file: " + e.Message);
        }

        // ---------- Reading from a file ----------
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                Console.WriteLine("Reading data from file:\n");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while reading from file: " + e.Message);
        }
        finally
        {
            Console.WriteLine("\nFile operation completed.");
        }
    }
}
