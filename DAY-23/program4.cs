// Program to demonstrate file handling 
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string path = "sample.txt";

        File.WriteAllText(path, "Hello File Handling");

        string content = File.ReadAllText(path);

        Console.WriteLine(content);
    }
}