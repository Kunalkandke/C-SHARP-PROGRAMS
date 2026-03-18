// 217. Program to find the majority element in an array using Boyer-Moore Voting Algorithm
using System;

class Program
{
    static int FindMajorityElement(int[] arr)
    {
        int count = 0, candidate = 0;

        foreach (int num in arr)
        {
            if (count == 0)
            {
                candidate = num;
                count = 1;
            }
            else
            {
                count += (num == candidate) ? 1 : -1;
            }
        }

        count = 0;
        foreach (int num in arr)
        {
            if (num == candidate)
                count++;
        }

        return (count > arr.Length / 2) ? candidate : -1;
    }

    static void Main(string[] args)
    {
        int[] arr = { 2, 2, 1, 1, 2, 2, 2 };
        int majority = FindMajorityElement(arr);

        if (majority != -1)
            Console.WriteLine("Majority element is: " + majority);
        else
            Console.WriteLine("No majority element found.");
    }
}