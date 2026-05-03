// Program to calculate the MD5 Checksum Hash of a file
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        string filePath = "example.txt";

        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        using (MD5 md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(fs);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            Console.WriteLine("MD5 Checksum: " + sb.ToString());
        }
    }
}