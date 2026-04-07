using System.Runtime.CompilerServices;
using BookCatalogAPI.Data;
using BookCatalogAPI.Dto.Author;
using BookCatalogAPI.Models;
using BookCatalogAPI.Services.Author;
using Microsoft.EntityFrameworkCore;
using BookCatalogAPI.Logs;

namespace BookCatalogAPI.Services
{
    public class AuthorService : IAuthorInterface
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<AuthorModel>>> ListAuthors()
        {
            ApiResponse<List<AuthorModel>> response = new ApiResponse<List<AuthorModel>>();

            try
            {
                
                var authors = await _context.Authors.ToListAsync();

                response.Data = authors;
                response.Message = "Authors listed.";
                response.Status = true;


                return response;

            } 
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ApiResponse<AuthorModel>> SearchAuthorById(int IdAuthor)
        {
            ApiResponse<AuthorModel> response = new ApiResponse<AuthorModel>();
            

            try
            {

                var author = await _context.Authors.FirstOrDefaultAsync(authorBanco => authorBanco.Id == IdAuthor);

                if (author == null)
                {
                    response.Message = "No Authors found with the ID.";
                    response.Status = false;
                    
                    return response;
                }

                response.Data = author;
                response.Message = "Author listed!";
                response.Status = true;

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ApiResponse<AuthorModel>> SearchAuthorByIdBook(int IdBook)
        {
            ApiResponse<AuthorModel> response = new ApiResponse<AuthorModel>();

            try
            {
                var author = await _context.Books
                    .Where(l=> l.Id == IdBook)
                    .Select(l => l.Author)
                    .FirstOrDefaultAsync();  

                if (author == null)
                {
                    response.Message = "Book or author not found";
                    response.Status = false;
                    
                    return response;
                }

                response.Data = author;
                response.Message = "Author located";
                response.Status = true;

                return response;
            
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }

        }

        public async Task<ApiResponse<AuthorModel>> CreateAuthor(CreateAuthorDto authorCriacaoDto)
        {
            ApiResponse<AuthorModel> response = new ApiResponse<AuthorModel>();

            try
            {
                var author = new AuthorModel()
                {
                    Name = authorCriacaoDto.Name,
                    LastName = authorCriacaoDto.LastName
                };

                _context.Add(author);
                await _context.SaveChangesAsync();

                response.Data = author;
                response.Message = "Author created";
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

        public async Task<ApiResponse<List<AuthorModel>>> UpdateAuthor(UpdateAuthorDto authorEdicaoDto)
        {
            ApiResponse<List<AuthorModel>> response = new ApiResponse<List<AuthorModel>>();

            try
            {
         
                var author = await _context.Authors
                    .FirstOrDefaultAsync(authorBanco => authorBanco.Id == authorEdicaoDto.Id);

                if(author == null)
                {
                    response.Message = $"No author found with Id {authorEdicaoDto.Id}";
                    response.Status = false;

                    return response;
                }

                author.Name = authorEdicaoDto.Name;
                author.LastName = authorEdicaoDto.LastName;

                _context.Update(author);
                await _context.SaveChangesAsync();

                response.Data = await _context.Authors.ToListAsync();
                response.Message = "Author updated";

                return response;

            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;

                return response;
            }
        }

        public async Task<ApiResponse<List<AuthorModel>>> DeleteAuthor(int IdAuthor)
        {

            ApiResponse<List<AuthorModel>> response = new ApiResponse<List<AuthorModel>>();

            try
            {
               var author = await _context.Authors
                    .FirstOrDefaultAsync(authorBanco => authorBanco.Id == IdAuthor);
                
                if(author == null)
                {
                    response.Message = $"No author found with Id {IdAuthor}";
                    response.Status = false;

                    return response;
                }

                _context.Remove(author);
                await _context.SaveChangesAsync();

                response.Data = await _context.Authors.ToListAsync();
                response.Message = "Author deleted";

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