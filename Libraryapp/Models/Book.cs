namespace Libraryapp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }
    }
}
