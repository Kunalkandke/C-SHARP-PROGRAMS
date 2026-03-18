// 215. Program to sort an array using the Merge Sort algorithm
using System;

class Program
{
    static void MergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;
            MergeSort(arr, left, mid);
            MergeSort(arr, mid + 1, right);
            Merge(arr, left, mid, right);
        }
    }

    static void Merge(int[] arr, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] L = new int[n1];
        int[] R = new int[n2];

        for (int i = 0; i < n1; i++)
            L[i] = arr[left + i];
        for (int j = 0; j < n2; j++)
            R[j] = arr[mid + 1 + j];

        int k = left, x = 0, y = 0;

        while (x < n1 && y < n2)
        {
            if (L[x] <= R[y])
            {
                arr[k] = L[x];
                x++;
            }
            else
            {
                arr[k] = R[y];
                y++;
            }
            k++;
        }

        while (x < n1)
        {
            arr[k] = L[x];
            x++; k++;
        }

        while (y < n2)
        {
            arr[k] = R[y];
            y++; k++;
        }
    }

    static void Main(string[] args)
    {
        int[] arr = { 12, 11, 13, 5, 6, 7 };
        MergeSort(arr, 0, arr.Length - 1);

        Console.WriteLine("Sorted array:");
        foreach (int item in arr)
            Console.Write(item + " ");
    }
}