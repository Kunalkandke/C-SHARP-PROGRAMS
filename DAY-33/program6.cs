// Program to create a custom Generic Collection (e.g., MyCustomList<T>)
using System;
using System.Collections;
using System.Collections.Generic;

class MyCustomList<T> : IEnumerable<T>
{
    private T[] items;
    private int count;

    public MyCustomList()
    {
        items = new T[4];
        count = 0;
    }

    public void Add(T item)
    {
        if (count == items.Length)
        {
            Array.Resize(ref items, items.Length * 2);
        }
        items[count++] = item;
    }

    public bool Remove(T item)
    {
        int index = Array.IndexOf(items, item, 0, count);
        if (index < 0) return false;

        for (int i = index; i < count - 1; i++)
        {
            items[i] = items[i + 1];
        }
        items[count - 1] = default(T);
        count--;
        return true;
    }

    public int Count => count;

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException();
            return items[index];
        }
        set
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException();
            items[index] = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
            yield return items[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

class Program
{
    static void Main()
    {
        MyCustomList<int> list = new MyCustomList<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);

        Console.WriteLine("Custom List Elements:");
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }

        list.Remove(20);
        Console.WriteLine("After removing 20:");
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("Element at index 1: " + list[1]);
    }
}