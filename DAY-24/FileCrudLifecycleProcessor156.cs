using System;
using System.IO;

class Program
{
    static void Main()
    {
        string file = "data.txt";

        File.WriteAllText(file, "First Record");

        Console.WriteLine("Data Written");

        string content = File.ReadAllText(file);

        Console.WriteLine("Data Read: " + content);

        File.AppendAllText(file, "\nSecond Record");

        Console.WriteLine("Data Updated");

        File.Delete(file);

        Console.WriteLine("File Deleted");
    }
}