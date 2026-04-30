// Program to perform a Left Outer Join between two lists using LINQ
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> list1 = new List<int> { 1, 2, 3, 4 };
        List<int> list2 = new List<int> { 3, 4, 5, 6 };

        var leftOuterJoin = from l1 in list1
                            join l2 in list2 on l1 equals l2 into temp
                            from t in temp.DefaultIfEmpty()
                            select new { Left = l1, Right = t };

        Console.WriteLine("Left Outer Join Result:");
        foreach (var item in leftOuterJoin)
        {
            string rightValue = item.Right == 0 ? "null" : item.Right.ToString();
            Console.WriteLine($"Left: {item.Left}, Right: {rightValue}");
        }
    }
}