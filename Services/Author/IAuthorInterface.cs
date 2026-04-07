using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCatalogAPI.Dto.Author;
using BookCatalogAPI.Models;

namespace BookCatalogAPI.Services.Author
{
    public interface IAuthorInterface
    {
        Task<ApiResponse<List<AuthorModel>>> ListAuthors();
        Task<ApiResponse<AuthorModel>> SearchAuthorById(int IdAuthor);
        Task<ApiResponse<AuthorModel>> SearchAuthorByIdBook(int IdBook);
        Task<ApiResponse<AuthorModel>> CreateAuthor(CreateAuthorDto authorCriacaoDto);
        Task<ApiResponse<List<AuthorModel>>> UpdateAuthor(UpdateAuthorDto authorEdicaoDto);
        Task<ApiResponse<List<AuthorModel>>> DeleteAuthor(int IdAuthor);
    }
}