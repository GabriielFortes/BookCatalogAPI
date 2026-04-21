using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using BookCatalogAPI.Data;
using BookCatalogAPI.Dto.Book;
using BookCatalogAPI.Models;
using BookCatalogAPI.Services.Book;
using Microsoft.EntityFrameworkCore;
using BookCatalogAPI.Logs;


namespace BookCatalogAPI.Services.Book
{
    public class BookService : IBookInterface
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context; 
        }

        public BookService()
        {
        }

        public async Task<ApiResponse<List<BookModel>>> ListBooks()
        {
            ApiResponse<List<BookModel>> response = new ApiResponse<List<BookModel>>();

            try
            {
                var books = await _context.Books.ToListAsync();

                response.Data = books;
                response.Message  = "Books listed.";
                response.Status = true;

                return response;
                throw new E
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ApiResponse<List<BooksDto>>> SearchBooksByIdAuthor(int IdAuthor)
        {
            ApiResponse<List<BooksDto>> response = new ApiResponse<List<BooksDto>>();

            try
            {
                
                var author = await _context.Authors
                    .FirstOrDefaultAsync(authorBanco => authorBanco.Id == IdAuthor);

                if(author == null)
                {
                    response.Message = "Author not located";
                    response.Status = false;

                    return response;
                }

                var books = await _context.Books
                    .Where(l => l.AuthorId == IdAuthor)
                    .Select(l => new BooksDto
                    {
                        Id = l.Id,
                        Title = l.Title,
                        NameAuthor = l.Author.Name
                    })
                    .ToListAsync();

                if(!books.Any())
                {
                    response.Message = $"No books found for the author {author.Name}";
                    response.Status = false;

                    return response;
                }

                response.Data = books;
                response.Message = "Books listed";
                response.Status = true;

                return response;

            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ApiResponse<BookModel>> ListBookById(int IdBook)
        {
            ApiResponse<BookModel> response = new ApiResponse<BookModel>();

            try
            {
                
                var book = await _context.Books.FirstOrDefaultAsync(bookBanco => bookBanco.Id == IdBook);

                if(book == null)
                {
                    response.Message = "No value found.";
                    response.Status = false;

                    return response;
                }

                response.Data = book;
                response.Message = "Book listed.";
                response.Status = true;

                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ApiResponse<BookModel>> CreateBook(CreateBookDto CreateBookDto)
        {
            ApiResponse<BookModel> response = new ApiResponse<BookModel>();

            try
            {
                var author = await _context.Authors
                    .FirstOrDefaultAsync(a => a.Id == CreateBookDto.AuthorId);

                if(author == null)
                {
                    response.Message = "Author not located";
                    response.Status = false;
                    
                    return response;
                }

                var book = new BookModel()
                {
                    Title = CreateBookDto.Title,
                    AuthorId = author.Id2
                };

                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                response.Data = book;
                response.Message = "Book created";
                response.Status = true;

                return response;

            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ApiResponse<BookModel>> UpdateBook(UpdateBookDto UpdateBookDto)
        {
            ApiResponse<BookModel> response = new ApiResponse<BookModel>();

            try
            {
                
                var book = await _context.Books
                    .FirstOrDefaultAsync(bookBanco => bookBanco.Id == UpdateBookDto.AuthorId);

                if(book == null)
                {
                    response.Message = $"No books found with the ID: {UpdateBookDto.AuthorId}";
                    response.Status = false;

                    return response;
                }

                book.Title = UpdateBookDto.Title;
                book.AuthorId = UpdateBookDto.AuthorId;

                _context.Update(book);
                await _context.SaveChangesAsync();

                response.Message = "Book updated.";
                response.Status = true;
                
                return response;

            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ApiResponse<BookModel>> DeleteBook(int IdBook)
        {
            
            ApiResponse<BookModel> response = new ApiResponse<BookModel>();

            try
            {
                
                var book = await _context.Books
                    .FirstOrDefaultAsync(bookBanco =>  bookBanco.Id == IdBook);

                if(book == null)
                {
                    response.Message = $"No books with ID {IdBook} listed for removal.";
                    response.Status = false;

                    return response STA;
                }

                _context.Remove(book);
                await _context.SaveChangesAsync();
                
                response.Message = $"Book {IdBook} deleted.";
                response.Status = true;

                return response;

            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

    }
}