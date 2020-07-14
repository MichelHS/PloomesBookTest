using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ploomes.ApiContext.Context;
using Ploomes.Services;
using Ploomes.Services.Handlers;
using System;
using System.Collections.Generic;

namespace Ploomes.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly PloomesContext _context;

        public BookController(PloomesContext context)
        {
            _context = context;
        }

        //controller url
        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                List<BookHandler> books = new BookServices(_context).GetBooks();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookDetailsCall bookDetails)
        {
            try
            {
                if (bookDetails == null) return BadRequest("informe os dados do livro");

                new BookServices(_context).PostBook(bookDetails);

                return Ok();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult Put(int bookId, [FromBody] BookDetailsCall bookDetails)
        {
            try
            {
                bool response = new BookServices(_context).PutBook(bookId, bookDetails);

                if (response == false) return NoContent();

                return Ok();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int bookId)
        {
            try
            {
                bool response = new BookServices(_context).DeleteBook(bookId);

                if (response == false) return NoContent();

                return Ok();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }


        //with endpoints
        [HttpGet]
        [Route("details")]
        public IActionResult GetBookDetails([FromQuery] int bookId)
        {
            try
            {
                BookDetails bookDetails = new BookServices(_context).GetBookDetails(bookId);

                if (bookDetails == null) return BadRequest("livro não encontrado");

                return Ok(bookDetails);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("genres")]
        public IActionResult GetTypes()
        {
            try
            {
                List<BookGenreHandler> bookGenres = new BookServices(_context).GetBookGenre();

                return Ok(bookGenres);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("uninitiated")]
        public IActionResult GetUninitiated()
        {
            try
            {
                List<BookHandler> books = new BookServices(_context).GetUninitiatedBooks();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("initiated")]
        public IActionResult GetInitiated()
        {
            try
            {
                List<BookHandler> books = new BookServices(_context).GetInitiatedBooks();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("finalized")]
        public IActionResult GetFinalized()
        {
            try
            {
                List<BookHandler> books = new BookServices(_context).GetFinalizedBooks();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("wishlist")]
        public IActionResult GetWishlist()
        {
            try
            {
                List<BookHandler> books = new BookServices(_context).GetBookWishlist();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}