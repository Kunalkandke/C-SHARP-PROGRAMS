using System;
using System.Collections;

class Program
{
    static void Main()
    {
        ArrayList list = new ArrayList();

        list.Add("Amit");
        list.Add(100);
        list.Add(3.14);

        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }
}