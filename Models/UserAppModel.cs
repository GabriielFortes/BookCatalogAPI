using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookCatalogAPI.Models
{
    public class UserAppModel
    {
        public int Id { get; set; }
        public string Email { get; set; }        
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}