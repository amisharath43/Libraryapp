using System.ComponentModel.DataAnnotations;

namespace Libraryapp.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Branch { get; set; }

    }
}

