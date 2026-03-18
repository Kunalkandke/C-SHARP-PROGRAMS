// 212. Program to perform Depth-First Search (DFS) on a Graph
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
            adjacencyList[i] = new List<int>();
    }

    public void AddEdge(int v, int w)
    {
        adjacencyList[v].Add(w);
    }

    public void DFS(int start)
    {
        bool[] visited = new bool[vertices];
        DFSUtil(start, visited);
    }

    private void DFSUtil(int v, bool[] visited)
    {
        visited[v] = true;
        Console.Write(v + " ");

        foreach (int neighbor in adjacencyList[v])
        {
            if (!visited[neighbor])
                DFSUtil(neighbor, visited);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Graph g = new Graph(6);
        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 3);
        g.AddEdge(1, 4);
        g.AddEdge(2, 4);
        g.AddEdge(3, 5);
        g.AddEdge(4, 5);

        Console.WriteLine("Depth-First Search starting from vertex 0:");
        g.DFS(0);
    }
}