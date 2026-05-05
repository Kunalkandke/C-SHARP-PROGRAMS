// Program to batch-process items asynchronously (chunking)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        List<int> items = Enumerable.Range(1, 20).ToList();
        int batchSize = 5;

        var batches = items
            .Select((item, index) => new { item, index })
            .GroupBy(x => x.index / batchSize)
            .Select(g => g.Select(x => x.item).ToList())
            .ToList();

        foreach (var batch in batches)
        {
            await ProcessBatchAsync(batch);
        }

        Console.WriteLine("All batches processed.");
    }

    static async Task ProcessBatchAsync(List<int> batch)
    {
        Console.WriteLine($"Processing batch: {string.Join(", ", batch)}");
        await Task.Delay(1000); // Simulate async work
    }
}