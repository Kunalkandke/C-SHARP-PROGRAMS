// 216. Program to implement Binary Search on a sorted array (Iterative and Recursive)
using System;

class Program
{
    static int BinarySearchIterative(int[] arr, int target)
    {
        int left = 0, right = arr.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (arr[mid] == target)
                return mid;
            else if (arr[mid] < target)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1;
    }

    static int BinarySearchRecursive(int[] arr, int left, int right, int target)
    {
        if (left > right)
            return -1;

        int mid = left + (right - left) / 2;
        if (arr[mid] == target)
            return mid;
        else if (arr[mid] < target)
            return BinarySearchRecursive(arr, mid + 1, right, target);
        else
            return BinarySearchRecursive(arr, left, mid - 1, target);
    }

    static void Main(string[] args)
    {
        int[] arr = { 1, 3, 5, 7, 9, 11 };
        int target = 7;

        int iterativeResult = BinarySearchIterative(arr, target);
        int recursiveResult = BinarySearchRecursive(arr, 0, arr.Length - 1, target);

        Console.WriteLine("Iterative Binary Search: Index of " + target + " is " + iterativeResult);
        Console.WriteLine("Recursive Binary Search: Index of " + target + " is " + recursiveResult);
    }
}