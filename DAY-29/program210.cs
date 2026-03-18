// Program to implement a Graph using an Adjacency List.
using System;
using System.Collections.Generic;

class Graph
{
    private int vertices;
    private List<int>[] adjacencyList;

    public Graph(int v)
    {
        vertices = v;
        adjacencyList = new List<int>[v];
        for (int i = 0; i < v; i++)
        {
            adjacencyList[i] = new List<int>();
        }
    }

    public void AddEdge(int src, int dest)
    {
        adjacencyList[src].Add(dest);
        adjacencyList[dest].Add(src); // For undirected graph
    }

    public void Display()
    {
        for (int i = 0; i < vertices; i++)
        {
            Console.Write("Vertex " + i + ":");
            foreach (int node in adjacencyList[i])
            {
                Console.Write(" -> " + node);
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Graph g = new Graph(5);

        g.AddEdge(0, 1);
        g.AddEdge(0, 4);
        g.AddEdge(1, 2);
        g.AddEdge(1, 3);
        g.AddEdge(1, 4);
        g.AddEdge(2, 3);
        g.AddEdge(3, 4);

        g.Display();
    }
}