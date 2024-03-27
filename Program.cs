// See https://aka.ms/new-console-template for more information


using System;
using Book.CRUD.Broker.Storeage;
using Book.CRUD.Models;
using Book.CRUD.Service;

IBookService bookService = new BookService();
//var book = bookService.GetBook(1);
//Console.WriteLine($"{book.Id}. {book.Name} {book.Author}");
bool isContinue = true;
do
{
    Console.WriteLine("1. Get All");
    Console.WriteLine("2. Add Book");
    Console.WriteLine("3. Update one book");
    Console.Write("Enter command: ");
    string command = Console.ReadLine();
    if (command.Contains("2") is true)
    {
        Books bookInfo = new Books();
        Console.Write("Enter book id: ");
        bookInfo.Id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter book name: ");
        bookInfo.Name = Console.ReadLine();
        Console.Write("Enter book author: ");
        bookInfo.Author = Console.ReadLine();

        bookService.InsertBook(bookInfo);
    }
    if (command.Contains("1") is true)
    {
        bookService.ReadAllBook();
    }
    if (command.Contains("3") is true)
    {
        bookService.Update(2,new Books() {Id = 3, Name = "Soadat asri", Author = "Muhammadyusuf"});
    }

    Console.Write("Is Continue");
    string isCommand = Console.ReadLine();
    if (isCommand.ToLower().Contains("no") is true)
    {
        isContinue = false;
    }
} while (isContinue is true);
