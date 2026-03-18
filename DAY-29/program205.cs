// Program to detect a cycle/loop in a Linked List (Floyd’s Cycle-Finding).
using System;

class Node
{
    public int Data;
    public Node Next;
    public Node(int data)
    {
        Data = data;
        Next = null;
    }
}

class LinkedList
{
    public Node Head;

    public void Add(int data)
    {
        Node newNode = new Node(data);
        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            Node temp = Head;
            while (temp.Next != null)
                temp = temp.Next;
            temp.Next = newNode;
        }
    }

    public bool HasCycle()
    {
        Node slow = Head;
        Node fast = Head;

        while (fast != null && fast.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;

            if (slow == fast)
                return true;
        }

        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        LinkedList list = new LinkedList();
        list.Add(10);
        list.Add(20);
        list.Add(30);
        list.Add(40);

        // Creating a cycle manually for testing
        list.Head.Next.Next.Next.Next = list.Head.Next;

        if (list.HasCycle())
            Console.WriteLine("Cycle detected in the linked list");
        else
            Console.WriteLine("No cycle in the linked list");
    }
}