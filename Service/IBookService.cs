using Book.CRUD.Models;

namespace Book.CRUD.Service
{
    internal interface IBookService
    {
        Books GetBook(int id);
        //ReadAllBook()
    }
}
