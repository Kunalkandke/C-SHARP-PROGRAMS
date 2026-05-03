// Program to use Memory-Mapped Files for high-performance reading
using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

class Program
{
    static void Main()
    {
        string filePath = "largefile.txt";

        using (var mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open))
        {
            using (var accessor = mmf.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read))
            {
                byte[] buffer = new byte[accessor.Capacity];
                accessor.ReadArray(0, buffer, 0, buffer.Length);

                string content = Encoding.UTF8.GetString(buffer);
                Console.WriteLine("Memory-Mapped File Content:");
                Console.WriteLine(content);
            }
        }
    }
}