using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCatalogAPI.Dto.Book
{
    public class BooksDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NameAuthor { get; set; }
    }
}