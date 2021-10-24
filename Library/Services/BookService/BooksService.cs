﻿using Library.DAL;
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
        public async Task<IServiceResponse> GetBook(int Id)
        {
            ServiceResponse<EditBookDTO> response = new ServiceResponse<EditBookDTO>();
            var dbBook = await _libraryContext.Books
                .FirstOrDefaultAsync(b => b.Id == Id);
            if (dbBook != null)
            {
                EditBookDTO book = new EditBookDTO() { Title = dbBook.Title, Author = dbBook.Author, ReleaseDate = dbBook.ReleaseDate, Description = dbBook.Description };
                response.Data = book;
                response.Message = "Succesfully retrieved book";
            }
            else
            {
                response.Success = false;
                response.Message = "There is no book in the database of given id";
            }
            return response;
        }
        public async Task<IServiceResponse> GetAllBooks()
        {
            ServiceResponse<List<BookDTO>> response = new ServiceResponse<List<BookDTO>>();

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
                        Reservation = dbBook.Reservation,
                        ReleaseDate = dbBook.ReleaseDate
                    });
                }
                response.Message = "Retrieved books successfully";
                response.Data = books;
            }
            else
            {
                response.Message = "There are no books in database";
                response.Success = false;
            }
            return response;
        }

        public async Task<IServiceResponse> AddBook(AddBookDTO addBookDTO)
        {
            ServiceResponse response = new ServiceResponse();
            
            Book book = new Book()
            {
                Title = addBookDTO.Title,
                Author = addBookDTO.Author,
                ReleaseDate = addBookDTO.ReleaseDate,
                Description = addBookDTO.Description,
            };
            await _libraryContext.Books.AddAsync(book);
            await _libraryContext.SaveChangesAsync();
            response.Message = "Succesfully added book to the database";
            return response;
        }
        public async Task<IServiceResponse> EditBook(EditBookDTO editBookDTO) {
            ServiceResponse response = new ServiceResponse();
            Book book = await _libraryContext.Books.FirstOrDefaultAsync(b => b.Id == editBookDTO.Id);
            book.Title = editBookDTO.Title;
            book.Author = editBookDTO.Author;
            book.ReleaseDate = editBookDTO.ReleaseDate;
            book.Description = editBookDTO.Description;
            _libraryContext.Books.Update(book);
            await _libraryContext.SaveChangesAsync();
            response.Message = "Successfully updated book";
            return response;
        }

        public async Task<IServiceResponse> ReserveBook(int bookId, int userId)
        {
            ServiceResponse response = new ServiceResponse();

            User user = await _libraryContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            Book book = await _libraryContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Couldn't find user";
            } else if (book == null || book.Reservation != null)
            {
                response.Success = false;
                response.Message = "Couldn't find book or book is already reserved";
            }
            if(user != null && book != null)
            {
                Reservation reservation = new Reservation()
                {
                    Book = book,
                    User = user,
                    ReservationDate = DateTime.Now
                };
                await _libraryContext.Reservations.AddAsync(reservation);
                await _libraryContext.SaveChangesAsync();
                response.Message = "Successfuly reserved the book";
            }
            return response;
        }

        public async Task<IServiceResponse> GetBooksReservations(int userId)
        {
            ServiceResponse<List<ReservationDTO>> response = new ServiceResponse<List<ReservationDTO>>();

            List<ReservationDTO> reservations = await _libraryContext.Reservations
            .Include(r => r.Book)
            .Where(r => r.UserId == userId)
            .Select(r => new ReservationDTO{ ReservationId=r.Id, ReservationDate = r.ReservationDate, Title = r.Book.Title, Author = r.Book.Author,ReleaseDate = r.Book.ReleaseDate, Description = r.Book.Description })
            .ToListAsync();

            response.Data = reservations;
            response.Message = "Successfully retrieved reservations";

            return response;
        }
    }
}
