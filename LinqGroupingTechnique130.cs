using System;
using System.Linq;

class Program
{
    static void Main()
    {
        string[] names = { "Amit", "Anita", "Rahul", "Ramesh", "Priya" };

        var grouped = names.GroupBy(name => name[0]);

        foreach (var group in grouped)
        {
            Console.WriteLine("Group: " + group.Key);
            foreach (var name in group)
            {
                Console.WriteLine("  " + name);
            }
        }
    }
}