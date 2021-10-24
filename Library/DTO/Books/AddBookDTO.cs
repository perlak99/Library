using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DTO.Books
{
    public class AddBookDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
    }
}
