// Program to read/write custom data to a Binary File
using System;
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
        string filePath = "products.bin";

        // Writing data to binary file
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Laptop", Price = 1200 },
            new Product { Id = 2, Name = "Mouse", Price = 25 },
            new Product { Id = 3, Name = "Keyboard", Price = 50 }
        };

        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            foreach (var p in products)
            {
                writer.Write(p.Id);
                writer.Write(p.Name);
                writer.Write(p.Price);
            }
        }
        Console.WriteLine("Data written to binary file.");

        // Reading data from binary file
        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                int id = reader.ReadInt32();
                string name = reader.ReadString();
                decimal price = reader.ReadDecimal();
                Console.WriteLine($"Id={id}, Name={name}, Price={price}");
            }
        }
    }
}