using System;
using System.Linq;

class Program
{
    static void Main()
    {
        var students = new[]
        {
            new { Id = 1, Name = "Amit" },
            new { Id = 2, Name = "Priya" }
        };

        var marks = new[]
        {
            new { StudentId = 1, Score = 90 },
            new { StudentId = 2, Score = 85 }
        };

        var result = students.Join(
            marks,
            s => s.Id,
            m => m.StudentId,
            (s, m) => new { s.Name, m.Score });

        foreach (var item in result)
        {
            Console.WriteLine(item.Name + " - " + item.Score);
        }
    }
}