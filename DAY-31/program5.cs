// Program to parse a URL string and extract query parameters into a Dictionary
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string url = "https://example.com/page?name=John&age=25&city=Paris";
        Dictionary<string, string> queryParams = new Dictionary<string, string>();

        int questionMarkIndex = url.IndexOf('?');
        if (questionMarkIndex != -1 && questionMarkIndex < url.Length - 1)
        {
            string queryString = url.Substring(questionMarkIndex + 1);
            string[] pairs = queryString.Split('&');

            foreach (var pair in pairs)
            {
                string[] keyValue = pair.Split('=');
                if (keyValue.Length == 2)
                    queryParams[keyValue[0]] = keyValue[1];
            }
        }

        Console.WriteLine("Query Parameters:");
        foreach (var kvp in queryParams)
        {
            Console.WriteLine($"{kvp.Key} : {kvp.Value}");
        }
    }
}