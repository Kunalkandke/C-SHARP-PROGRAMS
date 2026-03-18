// Program to implement a custom Doubly Linked List with Add/Remove nodes.
using System;

class Node
{
    public int Data;
    public Node Prev;
    public Node Next;

    public Node(int data)
    {
        Data = data;
        Prev = null;
        Next = null;
    }
}

class DoublyLinkedList
{
    private Node head;
    private Node tail;

    public void AddToEnd(int data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
    }

    public void AddToStart(int data)
    {
        Node newNode = new Node(data);
        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            newNode.Next = head;
            head.Prev = newNode;
            head = newNode;
        }
    }

    public void Remove(int data)
    {
        Node current = head;

        while (current != null)
        {
            if (current.Data == data)
            {
                if (current.Prev != null)
                    current.Prev.Next = current.Next;
                else
                    head = current.Next;

                if (current.Next != null)
                    current.Next.Prev = current.Prev;
                else
                    tail = current.Prev;

                return;
            }
            current = current.Next;
        }
        Console.WriteLine(data + " not found in the list.");
    }

    public void DisplayForward()
    {
        Node temp = head;
        Console.Write("List (forward): ");
        while (temp != null)
        {
            Console.Write(temp.Data + " ");
            temp = temp.Next;
        }
        Console.WriteLine();
    }

    public void DisplayBackward()
    {
        Node temp = tail;
        Console.Write("List (backward): ");
        while (temp != null)
        {
            Console.Write(temp.Data + " ");
            temp = temp.Prev;
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        DoublyLinkedList dll = new DoublyLinkedList();

        dll.AddToEnd(10);
        dll.AddToEnd(20);
        dll.AddToEnd(30);
        dll.DisplayForward();
        dll.DisplayBackward();

        dll.AddToStart(5);
        dll.DisplayForward();

        dll.Remove(20);
        dll.DisplayForward();

        dll.Remove(100); // element not in list
    }
}