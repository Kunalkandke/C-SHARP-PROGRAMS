using System;

class Program
{
    static void Main()
    {
        int[] arr = { 10, 20, 30, 40 };
        int search = 30;
        bool found = false;

        foreach (int i in arr)
        {
            if (i == search)
            {
                found = true;
                break;
            }
        }

        if (found)
            Console.WriteLine("Element found");
        else
            Console.WriteLine("Element not found");
    }
}
