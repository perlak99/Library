using Library.DTO.Books;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Library.Services.BookService;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;

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

        public IActionResult AddBook()
        {
            return View();
        }

        public IActionResult Reservations()
        {
            return View();
        }

        public async Task<IActionResult> EditBook(int id)
        {
            ServiceResponse<EditBookDTO> response = await _booksService.GetBook(id) as ServiceResponse<EditBookDTO>;
            if (!response.Success)
                return BadRequest(response);
            return View(response.Data);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookDTO AddBookDTO)
        {
            ServiceResponse response = await _booksService.AddBook(AddBookDTO) as ServiceResponse;
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditBook(EditBookDTO editBookDTO)
        {
            ServiceResponse response = await _booksService.EditBook(editBookDTO) as ServiceResponse;
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        public async Task<IActionResult> GetAllBooks()
        {
            ServiceResponse<List<BookDTO>> response = await _booksService.GetAllBooks() as ServiceResponse<List<BookDTO>>;
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReserveBook(int bookId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ServiceResponse response = await _booksService.ReserveBook(bookId, Int32.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value)) as ServiceResponse;
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ServiceResponse response = await _booksService.DeleteReservation(reservationId, Int32.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value)) as ServiceResponse;
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [Authorize]
        public async Task<IActionResult> GetReservations()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ServiceResponse<List<ReservationDTO>> response = await _booksService.GetBooksReservations(Int32.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value)) as ServiceResponse<List<ReservationDTO>>;
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
