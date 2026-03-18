// Program to implement a Priority Queue structure using a Heap.
using System;
using System.Collections.Generic;

class PriorityQueue
{
    private List<int> heap;

    public PriorityQueue()
    {
        heap = new List<int>();
    }

    private void Swap(int i, int j)
    {
        int temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (heap[parent] <= heap[index]) break;
            Swap(parent, index);
            index = parent;
        }
    }

    private void HeapifyDown(int index)
    {
        int left, right, smallest;
        while (true)
        {
            left = 2 * index + 1;
            right = 2 * index + 2;
            smallest = index;

            if (left < heap.Count && heap[left] < heap[smallest])
                smallest = left;
            if (right < heap.Count && heap[right] < heap[smallest])
                smallest = right;
            if (smallest == index) break;

            Swap(index, smallest);
            index = smallest;
        }
    }

    public void Enqueue(int item)
    {
        heap.Add(item);
        HeapifyUp(heap.Count - 1);
        Console.WriteLine(item + " enqueued");
    }

    public int Dequeue()
    {
        if (heap.Count == 0) throw new InvalidOperationException("Priority queue is empty");

        int root = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        if (heap.Count > 0)
            HeapifyDown(0);

        Console.WriteLine(root + " dequeued");
        return root;
    }

    public void Display()
    {
        Console.WriteLine("Priority Queue elements: " + string.Join(", ", heap));
    }
}

class Program
{
    static void Main(string[] args)
    {
        PriorityQueue pq = new PriorityQueue();

        pq.Enqueue(30);
        pq.Enqueue(20);
        pq.Enqueue(50);
        pq.Enqueue(10);
        pq.Display();

        pq.Dequeue();
        pq.Display();

        pq.Enqueue(5);
        pq.Display();
    }
}