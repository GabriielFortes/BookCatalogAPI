using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCatalogAPI.Dto.UserApp
{
    public class AuthenticateUserAppDto
    {
        public string Email { get; set; }
        public string password { get; set; }
    }
}