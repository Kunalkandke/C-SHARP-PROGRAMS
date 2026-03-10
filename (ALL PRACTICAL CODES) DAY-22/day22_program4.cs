using System;

class ArrayOperations
{
    static void Main()
    {
        // ---------- 1D ARRAY ----------
        Console.Write("Enter number of elements in 1D array: ");
        int n = Convert.ToInt32(Console.ReadLine());

        int[] arr = new int[n];

        Console.WriteLine("Enter elements of 1D array:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("\nElements of 1D array:");
        for (int i = 0; i < n; i++)
        {
            Console.Write(arr[i] + " ");
        }

        // ---------- SORTING (Bubble Sort) ----------
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("\n\nSorted 1D array (Ascending Order):");
        foreach (int item in arr)
        {
            Console.Write(item + " ");
        }

        // ---------- SEARCHING (Linear Search) ----------
        Console.Write("\n\nEnter element to search: ");
        int search = Convert.ToInt32(Console.ReadLine());
        bool found = false;

        for (int i = 0; i < n; i++)
        {
            if (arr[i] == search)
            {
                Console.WriteLine("Element found at position: " + (i + 1));
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Element not found.");
        }

        // ---------- 2D ARRAY ----------
        Console.Write("\nEnter number of rows for 2D array: ");
        int rows = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter number of columns for 2D array: ");
        int cols = Convert.ToInt32(Console.ReadLine());

        int[,] arr2D = new int[rows, cols];

        Console.WriteLine("Enter elements of 2D array:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                arr2D[i, j] = Convert.ToInt32(Console.ReadLine());
            }
        }

        Console.WriteLine("\nElements of 2D array:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(arr2D[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}
