using Library.DAL;
using Library.DTO.Books;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.BookService
{
    public class BooksService : IBooksService
    {
        readonly LibraryContext _libraryContext;

        public BooksService(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public Task<IServiceResponse> AddBook()
        {
            throw new NotImplementedException();
        }

        public async Task<IServiceResponse> GetAllBooks()
        {
            ServiceResponse<List<BookDTO>> sr = new ServiceResponse<List<BookDTO>>();

            var dbBooks = await _libraryContext.Books
                .Select(b => new { b.Id, b.Title, b.Author, b.Description, b.Reservation, b.ReleaseDate })
                .ToListAsync();
            if (dbBooks.Count() > 0)
            {
                List<BookDTO> books = new List<BookDTO>();
                foreach (var dbBook in dbBooks)
                {
                    books.Add(new BookDTO
                    {
                        Id = dbBook.Id,
                        Title = dbBook.Title,
                        Author = dbBook.Author,
                        Description = dbBook.Description,
                        Reservation= dbBook.Reservation,
                        ReleaseDate = dbBook.ReleaseDate
                    });
                }
                sr.Message = "Retrieved books successfully";
                sr.Data = books;
                sr.Success = true;
            }
            else
            {
                sr.Message = "There are no books in database";
                sr.Data = null;
                sr.Success = false;
            }
            return sr;
        }

        public Task<IServiceResponse> ShowReservations()
        {
            throw new NotImplementedException();
        }
    }
}
