// Program to traverse a BST in Inorder, Preorder, and Postorder.
using System;

class Node
{
    public int Data;
    public Node Left, Right;

    public Node(int data)
    {
        Data = data;
        Left = Right = null;
    }
}

class BST
{
    public Node Root;

    public void Insert(int data)
    {
        Root = InsertRec(Root, data);
    }

    private Node InsertRec(Node root, int data)
    {
        if (root == null)
            return new Node(data);

        if (data < root.Data)
            root.Left = InsertRec(root.Left, data);
        else if (data > root.Data)
            root.Right = InsertRec(root.Right, data);

        return root;
    }

    public void InOrder()
    {
        InOrderRec(Root);
        Console.WriteLine();
    }

    private void InOrderRec(Node root)
    {
        if (root != null)
        {
            InOrderRec(root.Left);
            Console.Write(root.Data + " ");
            InOrderRec(root.Right);
        }
    }

    public void PreOrder()
    {
        PreOrderRec(Root);
        Console.WriteLine();
    }

    private void PreOrderRec(Node root)
    {
        if (root != null)
        {
            Console.Write(root.Data + " ");
            PreOrderRec(root.Left);
            PreOrderRec(root.Right);
        }
    }

    public void PostOrder()
    {
        PostOrderRec(Root);
        Console.WriteLine();
    }

    private void PostOrderRec(Node root)
    {
        if (root != null)
        {
            PostOrderRec(root.Left);
            PostOrderRec(root.Right);
            Console.Write(root.Data + " ");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BST tree = new BST();

        tree.Insert(50);
        tree.Insert(30);
        tree.Insert(70);
        tree.Insert(20);
        tree.Insert(40);
        tree.Insert(60);
        tree.Insert(80);

        Console.WriteLine("In-order traversal:");
        tree.InOrder();

        Console.WriteLine("Pre-order traversal:");
        tree.PreOrder();

        Console.WriteLine("Post-order traversal:");
        tree.PostOrder();
    }
}