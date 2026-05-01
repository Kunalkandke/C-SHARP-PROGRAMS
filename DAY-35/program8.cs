// Program to serialize/deserialize an object to/from XML
using System;
using System.IO;
using System.Xml.Serialization;

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
        Product product = new Product { Id = 1, Name = "Laptop", Price = 1200 };

        string filePath = "product.xml";

        // Serialize to XML
        XmlSerializer serializer = new XmlSerializer(typeof(Product));
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(fs, product);
        }
        Console.WriteLine($"Serialized product to {filePath}");

        // Deserialize from XML
        Product deserializedProduct;
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            deserializedProduct = (Product)serializer.Deserialize(fs);
        }

        Console.WriteLine("Deserialized Product:");
        Console.WriteLine($"Id={deserializedProduct.Id}, Name={deserializedProduct.Name}, Price={deserializedProduct.Price}");
    }
}