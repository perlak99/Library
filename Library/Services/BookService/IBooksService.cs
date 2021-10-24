using Library.DTO.Books;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services.BookService
{
    public interface IBooksService
    {
        public Task<IServiceResponse> GetAllBooks();
        public Task<IServiceResponse> GetBook(int Id);
        public Task<IServiceResponse> AddBook(AddBookDTO addBookDTO);
        public Task<IServiceResponse> EditBook(EditBookDTO editBookDTO);
        public Task<IServiceResponse> ReserveBook(int bookId, int userId);
        public Task<IServiceResponse> DeleteReservation(int reservationId, int userId);
        public Task<IServiceResponse> GetBooksReservations(int userId);
    }
}
