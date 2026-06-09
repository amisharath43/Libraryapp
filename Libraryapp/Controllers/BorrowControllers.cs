using Libraryapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Libraryapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BorrowController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBorrowRecords()
        {
            var records = _context.BorrowRecords.ToList();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public IActionResult GetBorrowRecord(int id)
        {
            var record = _context.BorrowRecords.Find(id);
            if (record == null)
                return NotFound("Borrow record not found");
            return Ok(record);
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
                BookId = bookId,
                BorrowDate = DateTime.Now
            };

            book.IsAvailable = false;

            _context.BorrowRecords.Add(record);
            _context.SaveChanges();

            return Ok(new { message = "Book borrowed successfully", record });
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
            if (book != null)
                book.IsAvailable = true;

            _context.SaveChanges();

            return Ok(new { message = "Book returned successfully", record });
        }
    }
}