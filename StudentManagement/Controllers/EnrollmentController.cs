using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var enrollmentList = _context.Enrollment.Include(x => x.Students).Include(x => x.Course).Include(x=>x.Grade).ToList();
            return View(enrollmentList);
        }
    }
}
