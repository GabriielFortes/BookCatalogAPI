using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BookCatalogAPI.Dto.Author;
using BookCatalogAPI.Models;
using BookCatalogAPI.Services.Author;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorInterface _authorInterface;

        public AuthorController(IAuthorInterface authorInterface)
        {
            _authorInterface = authorInterface;
        }

        [Authorize]
        [HttpGet("ListAuthors")]
        public async Task<ActionResult<ApiResponse<List<AuthorModel>>>> ListAuthors()
        {
            var authors = await _authorInterface.ListAuthors();
            return Ok(authors);
        }

        [Authorize]
        [HttpGet("SearchAuthorById/{IdAuthor}")]
        public async Task<ActionResult<ApiResponse<AuthorModel>>> SearchAuthorById(int IdAuthor)
        {
            var author = await _authorInterface.SearchAuthorById(IdAuthor);
            return Ok(author);
        }

        [Authorize]
        [HttpGet("SearchAuthorByIdBook/{IdBook}")]
        public async Task<ActionResult<ApiResponse<AuthorModel>>> SearchAuthorByIdBook(int IdBook)
        {
            var author  = await _authorInterface.SearchAuthorByIdBook(IdBook);
            return Ok(author);
        }

        [Authorize]
        [HttpPost("CreateAuthor")]
        public async Task<ActionResult<ApiResponse<AuthorModel>>> CreateAuthor(CreateAuthorDto authorCriacaoDto)
        {
            var authors = await _authorInterface.CreateAuthor(authorCriacaoDto);
            return Ok(authors);
        }

        [Authorize]
        [HttpPut("UpdateAuthor")]
        public async Task<ActionResult<ApiResponse<AuthorModel>>> UpdateAuthor(UpdateAuthorDto authorEdicaoDto)
        {
            var authors = await _authorInterface.UpdateAuthor(authorEdicaoDto);
            return Ok(authors);
        }

        [Authorize]
        [HttpDelete("DeleteAuthor")]
        public async Task<ActionResult<ApiResponse<List<AuthorModel>>>> DeleteAuthor(int IdAuthor)
        {
            var authors = await _authorInterface.DeleteAuthor(IdAuthor);
            return Ok(authors);
        }


    }
}