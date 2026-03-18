// 213. Program to implement an LRU (Least Recently Used) Cache mechanism
using System;
using System.Collections.Generic;

class LRUCache<K, V>
{
    private int capacity;
    private Dictionary<K, LinkedListNode<(K key, V value)>> cacheMap;
    private LinkedList<(K key, V value)> cacheList;

    public LRUCache(int capacity)
    {
        this.capacity = capacity;
        cacheMap = new Dictionary<K, LinkedListNode<(K, V)>>();
        cacheList = new LinkedList<(K, V)>();
    }

    public V Get(K key)
    {
        if (cacheMap.ContainsKey(key))
        {
            var node = cacheMap[key];
            cacheList.Remove(node);
            cacheList.AddFirst(node);
            return node.Value.value;
        }
        throw new KeyNotFoundException("Key not found");
    }

    public void Put(K key, V value)
    {
        if (cacheMap.ContainsKey(key))
        {
            var node = cacheMap[key];
            cacheList.Remove(node);
        }
        else if (cacheMap.Count >= capacity)
        {
            var lru = cacheList.Last;
            cacheMap.Remove(lru.Value.key);
            cacheList.RemoveLast();
        }

        var newNode = new LinkedListNode<(K, V)>((key, value));
        cacheList.AddFirst(newNode);
        cacheMap[key] = newNode;
    }
}

class Program
{
    static void Main(string[] args)
    {
        LRUCache<int, string> lru = new LRUCache<int, string>(3);
        lru.Put(1, "One");
        lru.Put(2, "Two");
        lru.Put(3, "Three");

        Console.WriteLine(lru.Get(2)); // Access 2
        lru.Put(4, "Four"); // Evicts key 1

        try { Console.WriteLine(lru.Get(1)); } catch { Console.WriteLine("Key 1 not found"); }
        Console.WriteLine(lru.Get(3));
        Console.WriteLine(lru.Get(4));
    }
}