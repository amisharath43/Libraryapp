using Libraryapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Libraryapp.Controllers
{
    public class BorrowControllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class BorrowController : ControllerBase
        {
            private readonly LibraryContext _context;

            public BorrowController(LibraryContext context)
            {
                _context = context;
            }

            [HttpPost("borrow")]
            public IActionResult BorrowBook(int studentId, int bookId)
            {
                var book = _context.Books.Find(bookId);

                if (book == null || !book.IsAvailable)
                    return BadRequest("Book not available");

                var record = new BorrowRecord
                {
                    StudentId = studentId,
                    BookId = bookId
                };

                book.IsAvailable = false;

                _context.BorrowRecords.Add(record);
                _context.SaveChanges();

                return Ok("Book borrowed");
            }

            [HttpPost("return")]
            public IActionResult ReturnBook(int bookId)
            {
                var record = _context.BorrowRecords
                    .FirstOrDefault(x => x.BookId == bookId && x.ReturnDate == null);

                if (record == null)
                    return BadRequest("No active borrow record");

                record.ReturnDate = DateTime.Now;

                var book = _context.Books.Find(bookId);
                book.IsAvailable = true;

                _context.SaveChanges();

                return Ok("Book returned");
            }

        }
    }
}