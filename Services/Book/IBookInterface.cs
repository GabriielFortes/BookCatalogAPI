using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCatalogAPI.Dto.Book;
using BookCatalogAPI.Models;

namespace BookCatalogAPI.Services.Book
{
    public interface IBookInterface
    {
        Task<ApiResponse<List<BookModel>>> ListBooks();
        Task<ApiResponse<BookModel>> ListBookById(int IdBook);
        Task<ApiResponse<List<BooksDto>>> SearchBooksByIdAuthor(int IdAuthor);
        Task<ApiResponse<BookModel>> CreateBook(CreateBookDto CreateBookDto);
        Task<ApiResponse<BookModel>> UpdateBook(UpdateBookDto UpdateBookDto);
        Task<ApiResponse<BookModel>> DeleteBook(int IdBook);
    }
}