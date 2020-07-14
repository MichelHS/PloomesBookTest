using Ploomes.ApiContext.Context;
using Ploomes.ApiContext.Models;
using Ploomes.Services.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ploomes.Services
{
    public class BookServices
    {
        private readonly PloomesContext _context;
        public BookServices(PloomesContext context)
        {
            _context = context;
        }

        //GET
        public List<BookHandler> GetBooks()
        {
            try
            {
                List<BookHandler> books = (from book in _context.Book
                                           join genre in _context.BookGenre on book.BookGenreId equals genre.Id
                                           select new BookHandler
                                           {
                                               Id = book.Id,
                                               Genre = genre.Genre,
                                               Name = book.Name,
                                               Note = book.Note
                                           }).ToList();

                return books;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw;
            }
        }

        public BookDetails GetBookDetails(int bookId)
        {
            try
            {
                BookDetails bookDetails = (from book in _context.Book
                                           where book.Id == bookId
                                           select new BookDetails
                                           {
                                               Id = book.Id,
                                               BookGenreId = book.BookGenreId,
                                               Name = book.Name,
                                               Note = book.Note,
                                               FinishedReadingDate = book.FinishedReadingDate,
                                               StartedReadingDate = book.StartedReadingDate,
                                               PurchaseDate = book.PurchaseDate,
                                               StoppedPage = book.StoppedPage
                                           }).FirstOrDefault();

                return bookDetails;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw;
            }
        }

        public List<BookGenreHandler> GetBookGenre()
        {
            try
            {
                List<BookGenreHandler> bookGenres = (from genre in _context.BookGenre
                                                     select new BookGenreHandler
                                                     {
                                                         Id = genre.Id,
                                                         Genre = genre.Genre
                                                     }).ToList();

                return bookGenres;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw;
            }
        }

        public List<BookHandler> GetUninitiatedBooks()
        {
            try
            {
                List<BookHandler> books = (from book in _context.Book
                                           join genre in _context.BookGenre on book.BookGenreId equals genre.Id
                                           where book.StoppedPage == null && book.StartedReadingDate == null
                                           select new BookHandler
                                           {
                                               Id = book.Id,
                                               Genre = genre.Genre,
                                               Name = book.Name,
                                               Note = book.Note
                                           }).ToList();

                return books;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw;
            }
        }

        public List<BookHandler> GetInitiatedBooks()
        {
            try
            {
                List<BookHandler> books = (from book in _context.Book
                                           join genre in _context.BookGenre on book.BookGenreId equals genre.Id
                                           where book.StartedReadingDate != null || book.StoppedPage != null
                                           select new BookHandler
                                           {
                                               Id = book.Id,
                                               Genre = genre.Genre,
                                               Name = book.Name,
                                               Note = book.Note
                                           }).ToList();

                return books;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw;
            }
        }

        public List<BookHandler> GetFinalizedBooks()
        {
            try
            {
                List<BookHandler> books = (from book in _context.Book
                                           join genre in _context.BookGenre on book.BookGenreId equals genre.Id
                                           where book.FinishedReadingDate != null
                                           select new BookHandler
                                           {
                                               Id = book.Id,
                                               Genre = genre.Genre,
                                               Name = book.Name,
                                               Note = book.Note
                                           }).ToList();

                return books;

            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw;
            }
        }

        public List<BookHandler> GetBookWishlist()
        {
            try
            {
                List<BookHandler> books = (from book in _context.Book
                                           join genre in _context.BookGenre on book.BookGenreId equals genre.Id
                                           where book.PurchaseDate == null && book.StartedReadingDate == null
                                           select new BookHandler
                                           {
                                               Id = book.Id,
                                               Genre = genre.Genre,
                                               Name = book.Name,
                                               Note = book.Note
                                           }).ToList();

                return books;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw;
            }
        }

        //POST
        public void PostBook(BookDetailsCall bookDetails)
        {
            try
            {
                Book book = new Book()
                {
                    Name = bookDetails.Name,
                    BookGenreId = bookDetails.BookGenreId == null ? 43 : bookDetails.BookGenreId.GetValueOrDefault(),
                    Note = bookDetails.Note,
                    StartedReadingDate = bookDetails.StartedReadingDate,
                    FinishedReadingDate = bookDetails.FinishedReadingDate,
                    PurchaseDate = bookDetails.PurchaseDate,
                    StoppedPage = bookDetails.StoppedPage
                };

                _context.Book.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw;
            }
        }

        //PUT
        public bool PutBook(int bookId, BookDetailsCall bookDetails)
        {
            try
            {
                Book book = _context.Book.Where(x => x.Id == bookId).FirstOrDefault();

                if (book != null)
                {
                    book.Name = bookDetails.Name ?? book.Name;
                    book.BookGenreId = bookDetails.BookGenreId ?? book.BookGenreId;
                    book.Note = bookDetails.Note ?? book.Note;
                    book.StartedReadingDate = bookDetails.StartedReadingDate ?? book.StartedReadingDate;
                    book.FinishedReadingDate = bookDetails.FinishedReadingDate ?? book.FinishedReadingDate;
                    book.PurchaseDate = bookDetails.PurchaseDate ?? book.PurchaseDate;
                    book.StoppedPage = bookDetails.StoppedPage ?? book.StoppedPage;

                    _context.Book.Update(book);
                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw;
            }
        }

        //DELETE
        public bool DeleteBook(int bookId)
        {
            try
            {
                Book book = _context.Book.Where(x => x.Id == bookId).FirstOrDefault();

                if (book != null)
                {
                    _context.Book.Remove(book);
                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw;
            }
        }
    }
}
