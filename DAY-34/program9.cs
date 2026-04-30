// Program to demonstrate ObservableCollection<T> for UI updates
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

class Program
{
    static void Main()
    {
        ObservableCollection<string> items = new ObservableCollection<string>();
        items.CollectionChanged += Items_CollectionChanged;

        items.Add("Item 1");
        items.Add("Item 2");
        items.Remove("Item 1");
        items[0] = "Updated Item 2";
    }

    private static void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (var newItem in e.NewItems)
                Console.WriteLine($"Added: {newItem}");
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (var oldItem in e.OldItems)
                Console.WriteLine($"Removed: {oldItem}");
        }
        else if (e.Action == NotifyCollectionChangedAction.Replace)
        {
            foreach (var newItem in e.NewItems)
                Console.WriteLine($"Replaced with: {newItem}");
        }
    }
}