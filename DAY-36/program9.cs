// Program to safely read from a file while another process is writing to it
using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        string filePath = "sharedfile.txt";

        try
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
            {
                string content = reader.ReadToEnd();
                Console.WriteLine("File content:");
                Console.WriteLine(content);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error reading file: " + ex.Message);
        }
    }
}