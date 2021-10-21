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
        public Task<IServiceResponse> AddBook();
        public Task<IServiceResponse> ShowReservations();
    }
}
