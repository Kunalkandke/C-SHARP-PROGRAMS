// Program to write a List<T> of objects to a formatted CSV file
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
        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1200 },
            new Product { Id = 2, Name = "Mouse", Price = 25 },
            new Product { Id = 3, Name = "Keyboard", Price = 50 }
        };

        string filePath = "output.csv";
        WriteCsv(products, filePath);

        Console.WriteLine($"CSV file '{filePath}' created successfully.");
    }

    static void WriteCsv<T>(List<T> list, string path)
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            var props = typeof(T).GetProperties();

            // Write header
            sw.WriteLine(string.Join(",", Array.ConvertAll(props, p => p.Name)));

            // Write rows
            foreach (var item in list)
            {
                var values = Array.ConvertAll(props, p => p.GetValue(item)?.ToString() ?? "");
                sw.WriteLine(string.Join(",", values));
            }
        }
    }
}