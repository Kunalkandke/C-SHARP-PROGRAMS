using System;
using System.Text;

class Program
{
    static void Main()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Hello ");
        sb.Append("World");

        Console.WriteLine("StringBuilder Output: " + sb.ToString());
    }
}
