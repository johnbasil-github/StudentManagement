using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data;

namespace StudentManagement.Controllers
{
    public class GradeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GradeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var gradesList = _context.Grades.ToList();

            return View(gradesList);
        }
    }
}
