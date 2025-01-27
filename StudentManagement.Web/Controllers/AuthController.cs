using EStudentManagement.DataAccess.Data;
using EStudentManagement.Web.Models;
using EStudentManagement.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Web.Controllers {

    public class AuthController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext _db, IConfiguration configuration) {
            _context = _db;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user) {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if (user.Role == "Student") {
                Student student = new Student {
                    Name = user.Username,
                    DateOfBirth = DateTime.Now,
                };

                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            // Redirect to the home page
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user) {
            var obj = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);

            if (obj != null) {
                var jwtService = new JwtService(_configuration);

                var token = jwtService.GenerateToken(obj);
                ViewBag.user = obj.Username;

                if (Request.Cookies.TryGetValue("jwtToken", out string jwtToken)) {
                    Console.WriteLine(jwtToken + "...................");
                }

                Response.Cookies.Append("jwtToken", token, new CookieOptions {
                    HttpOnly = false, // Allow JavaScript access
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(0.1)
                });

                if (Request.Cookies.TryGetValue("jwtToken", out jwtToken)) {
                    Console.WriteLine(jwtToken + "...................");
                }
                HttpContext.Session.SetString("Username", obj.Username);
                HttpContext.Session.SetString("Role", obj.Role);

                if (obj.Role == "Admin") {
                    TempData["success"] = "Successfully logged in!";
                    return RedirectToAction("Index", "Admin");
                }
                else {
                    TempData["success"] = "Successfully logged in!";
                    return RedirectToAction("Index", "Student");
                }
            }

            TempData["error"] = "Invalid login credentials";
            return RedirectToAction("Login", "Auth");
        }
    }
}