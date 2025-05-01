using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;

namespace StudentManagement.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InstructorController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var instructorList = _context.Instructor.ToList();

            return View(instructorList);
        }
    }
}
