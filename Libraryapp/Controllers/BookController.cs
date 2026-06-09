using Libraryapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Libraryapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(_context.Books.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return NotFound("Book not found");
            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook == null)
                return NotFound("Book not found");

            existingBook.Title = book.Title;
            existingBook.Publisher = book.Publisher;
            existingBook.Genre = book.Genre;
            existingBook.IsAvailable = book.IsAvailable;

            _context.SaveChanges();
            return Ok(existingBook);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return NotFound("Book not found");

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok("Book deleted");
        }
    }
}
