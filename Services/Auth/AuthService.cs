using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookCatalogAPI.Data;
using BookCatalogAPI.Dto.UserApp;
using BookCatalogAPI.Models;
using BookCatalogAPI.Services.Auth;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;    
using BCrypt.Net;


namespace BookCatalogAPI
{
    public class AuthService : IAuthInterface
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ApiResponse<string>> Register(RegisterUserAppDto dto)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            try
            {
                var Email = dto.Email.ToLower();
                //verifica Email
                if (await _context.UserApp.AnyAsync(u => u.Email == Email))
                {
                    response.Message = "Email already registered";
                    response.Status = false;
                    return response;
                }
                
                var user = new UserAppModel
                {
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.password),
                    Role = "UserApp"
                };

                _context.UserApp.Add(user);
                await _context.SaveChangesAsync();

                response.Message = "UserApp successfully registered.";
                response.Data = Email;
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

        public async Task<ApiResponse<string>> Login(AuthenticateUserAppDto dto)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            try
            {
                var user = await _context.UserApp
                    .FirstOrDefaultAsync(u => u.Email == dto.Email);

                if(user == null || !BCrypt.Net.BCrypt.Verify(dto.password, user.PasswordHash))
                {
                    response.Message = "Email or password invalid";
                    response.Status = false;
                    return response;
                }

                response.Data = GenerateToken(user);
                response.Message = "Login successful";
                response.Status = true;

                return response;
            }
            catch(Exception ex)
            {
                response.Message = "Internal error. Contact the Administrator.";
                response.Status = false;

                return response;
            }
        }

        private string GenerateToken(UserAppModel user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}