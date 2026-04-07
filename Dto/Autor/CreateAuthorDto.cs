using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCatalogAPI.Dto.Author
{
    public class CreateAuthorDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}