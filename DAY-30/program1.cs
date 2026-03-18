// 211. Program to perform Breadth-First Search (BFS) on a Graph.
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

    public void BFS(int startVertex)
    {
        bool[] visited = new bool[vertices];
        Queue<int> queue = new Queue<int>();

        visited[startVertex] = true;
        queue.Enqueue(startVertex);

        while (queue.Count > 0)
        {
            int vertex = queue.Dequeue();
            Console.Write(vertex + " ");

            foreach (int neighbor in adjacencyList[vertex])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }
        Console.WriteLine();
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

        Console.WriteLine("BFS starting from vertex 0:");
        g.BFS(0);
    }
}
