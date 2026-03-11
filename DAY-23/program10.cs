// Console-based Library Management System
using System;
using System.Collections.Generic;

class Book
{
    public int Id;
    public string Title;
    public string Author;
}

class Program
{
    static List<Book> books = new List<Book>();

    static void AddBook()
    {
        Book b = new Book();

        Console.Write("Enter Book ID: ");
        b.Id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Book Title: ");
        b.Title = Console.ReadLine();

        Console.Write("Enter Author Name: ");
        b.Author = Console.ReadLine();

        books.Add(b);
    }

    static void ShowBooks()
    {
        foreach (Book b in books)
        {
            Console.WriteLine(b.Id + " " + b.Title + " " + b.Author);
        }
    }

    static void Main(string[] args)
    {
        int choice;

        do
        {
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Show Books");
            Console.WriteLine("3. Exit");

            Console.Write("Enter Choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
                AddBook();
            else if (choice == 2)
                ShowBooks();

        } while (choice != 3);
    }
}