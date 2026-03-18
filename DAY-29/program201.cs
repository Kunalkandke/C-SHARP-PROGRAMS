// Program to implement a Circular Queue using an array.
using System;

class CircularQueue
{
    private int[] queue;
    private int front, rear, size, capacity;

    public CircularQueue(int capacity)
    {
        this.capacity = capacity;
        queue = new int[capacity];
        front = -1;
        rear = -1;
        size = 0;
    }

    public void Enqueue(int item)
    {
        if (IsFull())
        {
            Console.WriteLine("Queue is full");
            return;
        }

        rear = (rear + 1) % capacity;
        queue[rear] = item;
        if (front == -1) front = rear;
        size++;
        Console.WriteLine(item + " enqueued");
    }

    public void Dequeue()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Queue is empty");
            return;
        }

        Console.WriteLine(queue[front] + " dequeued");
        front = (front + 1) % capacity;
        size--;
        if (size == 0) front = rear = -1;
    }

    public void Display()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Queue is empty");
            return;
        }

        Console.Write("Queue elements: ");
        for (int i = 0; i < size; i++)
        {
            int index = (front + i) % capacity;
            Console.Write(queue[index] + " ");
        }
        Console.WriteLine();
    }

    private bool IsFull()
    {
        return size == capacity;
    }

    private bool IsEmpty()
    {
        return size == 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        CircularQueue cq = new CircularQueue(5);

        cq.Enqueue(10);
        cq.Enqueue(20);
        cq.Enqueue(30);
        cq.Enqueue(40);
        cq.Enqueue(50);
        cq.Display();

        cq.Dequeue();
        cq.Dequeue();
        cq.Display();

        cq.Enqueue(60);
        cq.Enqueue(70);
        cq.Display();
    }
}
