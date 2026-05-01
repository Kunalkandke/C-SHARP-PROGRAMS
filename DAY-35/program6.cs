// Program to read a CSV File and map rows to a List<T>
using System;
using System.Collections.Generic;
using System.IO;

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

class Program
{
    static void Main()
    {
        string filePath = "products.csv"; // CSV file path

        List<Product> products = ReadCsv<Product>(filePath, line =>
        {
            var parts = line.Split(',');
            return new Product
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                Price = decimal.Parse(parts[2])
            };
        });

        Console.WriteLine("Products from CSV:");
        foreach (var p in products)
        {
            Console.WriteLine($"Id={p.Id}, Name={p.Name}, Price={p.Price}");
        }
    }

    static List<T> ReadCsv<T>(string path, Func<string, T> mapFunc)
    {
        List<T> list = new List<T>();
        foreach (var line in File.ReadLines(path))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            list.Add(mapFunc(line));
        }
        return list;
    }
}