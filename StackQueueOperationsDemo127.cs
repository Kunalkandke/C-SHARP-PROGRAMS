using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Stack (LIFO)
        Stack<int> stack = new Stack<int>();
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);

        Console.WriteLine("Stack Pop: " + stack.Pop());

        // Queue (FIFO)
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(100);
        queue.Enqueue(200);
        queue.Enqueue(300);

        Console.WriteLine("Queue Dequeue: " + queue.Dequeue());
    }
}