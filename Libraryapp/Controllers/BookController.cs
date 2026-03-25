using Libraryapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Libraryapp.Controllers
{
    public class BookController
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

            [HttpPost]
            public IActionResult AddBook(Book book)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return Ok(book);
            }
        }
    }
}
