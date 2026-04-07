using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCatalogAPI.Dto.UserApp;
using BookCatalogAPI.Models;
using BookCatalogAPI.Services.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalogAPI.Controllers
{
    public class AuthController
    {
        private readonly IAuthInterface _authService;

        public AuthController(IAuthInterface authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<string>>> Register(RegisterUserAppDto dto)
        {
            var response = await _authService.Register(dto);
            return response;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<ApiResponse<string>>> Authenticate(AuthenticateUserAppDto dto)
        {
            var response = await _authService.Login(dto);
            return response;
        }
    }
}