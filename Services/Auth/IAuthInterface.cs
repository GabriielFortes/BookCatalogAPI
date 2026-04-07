using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCatalogAPI.Dto.UserApp;
using BookCatalogAPI.Models;

namespace BookCatalogAPI.Services.Auth
{
    public interface IAuthInterface
    {
        Task<ApiResponse<string>> Register(RegisterUserAppDto dto);
        Task<ApiResponse<string>> Login(AuthenticateUserAppDto dto);
    }
}