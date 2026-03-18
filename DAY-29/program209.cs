// Program to calculate the maximum depth or height of a Binary Tree.
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

class BinaryTree
{
    public Node Root;

    public int MaxDepth(Node node)
    {
        if (node == null)
            return 0;

        int leftDepth = MaxDepth(node.Left);
        int rightDepth = MaxDepth(node.Right);

        return Math.Max(leftDepth, rightDepth) + 1;
    }
}

class Program
{
    static void Main(string[] args)
    {
        BinaryTree tree = new BinaryTree();
        tree.Root = new Node(1);
        tree.Root.Left = new Node(2);
        tree.Root.Right = new Node(3);
        tree.Root.Left.Left = new Node(4);
        tree.Root.Left.Right = new Node(5);

        int height = tree.MaxDepth(tree.Root);
        Console.WriteLine("Maximum depth/height of the binary tree: " + height);
    }
}