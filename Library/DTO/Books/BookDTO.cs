using Library.Models;
using System;

namespace Library.DTO.Books
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public Reservation Reservation { get; set; }
    }
}
