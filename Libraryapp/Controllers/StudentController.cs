using Libraryapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Libraryapp.Controllers
{
    public class StudentController : Controller
    {

        [ApiController]
        [Route("api/[controller]")]
        public class StudentsController : ControllerBase
        {
            private readonly LibraryContext _context;

            public StudentsController(LibraryContext context)
            {
                _context = context;
            }

            [HttpGet]
            public IActionResult GetStudents()
            {
                return Ok(_context.Students.ToList());
            }

            [HttpPost]
            public IActionResult AddStudent(Student student)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return Ok(student);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
