using Library.DTO.Books;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Library.Services.BookService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllBooks()
        {
            ServiceResponse<List<BookDTO>> response = await _booksService.GetAllBooks() as ServiceResponse<List<BookDTO>>;
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
