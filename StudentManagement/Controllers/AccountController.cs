using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;
using StudentManagement.ViewModels;

namespace StudentManagement.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        private ApplicationDbContext _dbContext;

        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
   
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    UserType = model.UserType
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.UserType == "Student")
                    {
                        var student = new Students
                        {

                            UserId = user.Id,
                            StudentName = user.FullName
                        };

                        _dbContext.Students.Add(student);
                        await _dbContext.SaveChangesAsync();
                    }
                    else if (model.UserType == "Instructor")
                    {
                        var instructor = new Instructor
                        {

                            UserId = user.Id,
                            InstructorName = user.FullName
                        };

                        _dbContext.Instructor.Add(instructor);
                        await _dbContext.SaveChangesAsync();
                    }

                    // Redirect to login or dashboard
                    return RedirectToAction("Login", "Account");

                }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    //model.RememberMe,
                    false,
                    false);

                if (result.Succeeded)
                {

                    var user = await _userManager.FindByEmailAsync(model.Email);


                    if (user.UserType == "Student")
                    {
                        return RedirectToAction("Index", "Student", new { userId = user.Id });
                    }
                    else if (user.UserType == "Instructor")
                    {
                        return RedirectToAction("Index", "Instructor", new { userId = user.Id });
                    }
                    else if (user.UserType == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    return RedirectToAction("Index", "Home"); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }


    }
}
