using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCatalogAPI.Dto.Book;
using BookCatalogAPI.Models;
using BookCatalogAPI.Services.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookInterface _bookInterface;

        public BookController(IBookInterface bookInterface)
        {
            _bookInterface = bookInterface;
        }

        [Authorize]
        [HttpGet("ListBooks")]
        public async Task<ActionResult<ApiResponse<List<AuthorModel>>>> ListBooks()
        {
            var books = await _bookInterface.ListBooks();
            return Ok(books);
        }

        [Authorize]
        [HttpGet("ListBookById/{IdBook}")]
        public async Task<ActionResult<ApiResponse<BookModel>>> ListBookById(int IdBook)
        {
            var book = await _bookInterface.ListBookById(IdBook);
            return Ok(book);
        }

        [Authorize]
        [HttpGet("SearchBooksByIdAuthor/{IdAuthor}")]
        public async Task<ActionResult<ApiResponse<List<BooksDto>>>> SearchBooksByIdAuthor(int IdAuthor)
        {
            var book = await _bookInterface.SearchBooksByIdAuthor(IdAuthor);
            return Ok(book);
        }        

        [Authorize]
        [HttpPost("CreateBook")]
        public async Task<ActionResult<ApiResponse<BookModel>>> CreateBook(CreateBookDto CreateBookDto)
        {
            var book = await _bookInterface.CreateBook(CreateBookDto);
            return book;
        }

        [Authorize]
        [HttpPut("UpdateBook")]
        public async Task<ActionResult<ApiResponse<BookModel>>> UpdateBook(UpdateBookDto UpdateBookDto)
        {
            var book = await _bookInterface.UpdateBook(UpdateBookDto);
            return book;
        }

        [Authorize]
        [HttpDelete("DeleteBook")]
        public async Task<ActionResult<ApiResponse<BookModel>>> DeleteBook(int IdBook)
        {
            var book = await _bookInterface.DeleteBook(IdBook);
            return Ok(book);
        }

    }
}