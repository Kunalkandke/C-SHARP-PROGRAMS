// 214. Program to sort an array using the Quick Sort algorithm
using System;

class Program
{
    static void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(arr, low, high);
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }

    static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        int temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;

        return i + 1;
    }

    static void Main(string[] args)
    {
        int[] arr = { 10, 7, 8, 9, 1, 5 };
        int n = arr.Length;

        QuickSort(arr, 0, n - 1);

        Console.WriteLine("Sorted array:");
        foreach (int item in arr)
            Console.Write(item + " ");
    }
}