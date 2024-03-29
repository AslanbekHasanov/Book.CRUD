﻿using Book.CRUD.Broker.Logging;
using Book.CRUD.Broker.Storeage;
using Book.CRUD.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Book.CRUD.Service
{
    internal class BookService : IBookService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IStoreageBroker storeageBroker;
        public BookService()
        {
            this.loggingBroker = new LoggingBroker();
            this.storeageBroker = new ArrayStoreageBroker();
        }
        public Books GetBook(int id)
        {
            Books book = this.storeageBroker.ReadBook(id);
            return book;
        }

        public Books InsertBook(Books book)
        {
            return book is null
                    ? InsertBookIsInvalid()
                    : ValidationAndInsertBook(book);
        }

        public Books[] ReadAllBook()
        {
            var bookInfo = this.storeageBroker.GetAllBook();
            if (bookInfo.Length is 0)
            {
                this.loggingBroker.LogError("Information not available.");
            }
            else
            {
                for (int itaration = 0; itaration < bookInfo.Length; itaration++)
                {
                    if (bookInfo[itaration] is not null)
                    {
                        this.loggingBroker.LogInformation($"{bookInfo[itaration].Id}. {bookInfo[itaration].Name} {bookInfo[itaration].Author}");
                    }
                }
            }

            return bookInfo;

        }

        public bool Update(int id, Books book)
        {
            return this.storeageBroker.Update(id, book);
        }

        private Books ValidationAndInsertBook(Books book)
        {
            if (book.Id is 0
                || String.IsNullOrWhiteSpace(book.Name)
                || String.IsNullOrWhiteSpace(book.Author))
            {
                this.loggingBroker.LogError("Invalid books inforamtion.");
                return new Books();
            }
            else
            {
                var bookInfo = this.storeageBroker.AddBook(book);

                if (bookInfo is null)
                {
                    this.loggingBroker.LogInformation("Not Added book Info.");
                }
                else
                {
                    this.loggingBroker.LogInformation("Secssesfull.");
                }
                return bookInfo;
            }
        }

        private Books InsertBookIsInvalid()
        {
            this.loggingBroker.LogError("Book info is null.");
            return new Books();
        }
    }
}
