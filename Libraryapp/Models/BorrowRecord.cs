using System.ComponentModel.DataAnnotations;

namespace Libraryapp.Models
{
    public class BorrowRecord
    {
        [Key]
        public int BorrowId { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
