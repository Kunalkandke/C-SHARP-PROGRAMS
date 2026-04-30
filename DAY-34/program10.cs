// Program to convert a List<T> of custom objects into an ADO.NET DataTable
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

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

        DataTable dt = ToDataTable(products);

        Console.WriteLine("DataTable Contents:");
        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn col in dt.Columns)
            {
                Console.Write($"{col.ColumnName}={row[col]} ");
            }
            Console.WriteLine();
        }
    }

    static DataTable ToDataTable<T>(List<T> items)
    {
        DataTable table = new DataTable(typeof(T).Name);
        PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in props)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

        foreach (var item in items)
        {
            var values = new object[props.Length];
            for (int i = 0; i < props.Length; i++)
                values[i] = props[i].GetValue(item, null);
            table.Rows.Add(values);
        }

        return table;
    }
}