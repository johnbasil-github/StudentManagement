using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
