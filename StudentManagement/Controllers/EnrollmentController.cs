using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;
        public EnrollmentController(UserManager<ApplicationUser>userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var enrollmentList = _context.Enrollment.Include(x => x.Students).Include(x => x.Course).ToList();
            return View(enrollmentList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Enrollment enrollment)
        {
            _context.Enrollment.Add(enrollment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            //var person = _context.Enrollment.Include(x => x.Students).Include(x => x.Course).Find(id);
            var person = _context.Enrollment.Include(x => x.Students).Include(x => x.Course).FirstOrDefault(x => x.EnrollmentId == id);
            return View(person);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var person = _context.Enrollment.Find(id);
            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(Enrollment enrollment)
        {
            _context.Enrollment.Update(enrollment);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            var person = _context.Enrollment.Find(id);

            _context.Enrollment.Remove(person);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Enroll(int courseId)
        {
            // Get logged-in user ID
            var userId = _userManager.GetUserId(User);

            // Find the matching student record
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            // Check if already enrolled
            bool alreadyEnrolled = _context.Enrollment
                .Any(e => e.StudentId == student.StudentId && e.CourseId == courseId);

            if (alreadyEnrolled)
            {
                TempData["Message"] = "Already enrolled in this course.";
                return RedirectToAction("View", "Course");
            }

            // Enroll student
            var enrollment = new Enrollment
            {
                CourseId = courseId,
                StudentId = student.StudentId,
                EnrollmentDate = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.Enrollment.Add(enrollment);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Successfully enrolled in course.";
            return RedirectToAction("View", "Course");
        }


        public async Task<IActionResult> MyEnrollments()
        {
            var userId = _userManager.GetUserId(User);
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            var enrollments = _context.Enrollment
                .Include(e => e.Course)
                .Where(e => e.StudentId == student.StudentId)
                .ToList();

            return View(enrollments);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteEnrollment(int enrollmentId)
        {
            var enrollment = await _context.Enrollment.FindAsync(enrollmentId);
            if (enrollment != null)
            {
                _context.Enrollment.Remove(enrollment);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Enrollment deleted successfully.";
            }
            else
            {
                TempData["Error"] = "Enrollment not found.";
            }

            return RedirectToAction("MyEnrollments");
        }



    }
}
