using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var courseList = _context.Course.Include( x => x.Instructor).ToList();

            return View(courseList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Course course)
        {
            _context.Course.Update(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        public IActionResult Details(int id)
        {
            var person = _context.Course.Include(x => x.Instructor).FirstOrDefault(x => x.CourseId == id);

            return View(person);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var person = _context.Course.Find(id);
            return View(person);
        }

        public IActionResult Edit(Course course)
        {
            _context.Course.Update(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var person = _context.Course.Find(id);

            _context.Course.Remove(person);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
