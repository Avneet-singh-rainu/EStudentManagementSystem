using EStudentManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Web.Data;

namespace StudentManagement.Web.Controllers {

    public class AdminController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public AdminController(ApplicationDbContext _db, ILogger<AdminController> logger) {
            _context = _db;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult AddStudent() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student) {
            if (ModelState.IsValid) {
                // Create a new User
                var user = new User {
                    Username = student.Name,
                    Password = student.Name + "@123",
                    Role = "Student"  // Set the Role as "Student"
                };

                // Add the User to the database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();  // Save the User to generate UserId

                // Add the Student to the database
                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                TempData["success"] = "Student added successfully!";
                return RedirectToAction(nameof(GetAllStudents));  // Redirect to the student list page
            }

            // If validation fails, return to the AddStudent view with the model
            return View(GetAllStudents);
        }

        [HttpGet]
        public IActionResult GetAllStudents() {
            var students = _context.Students.ToList();
            return View(students);
        }

        [HttpGet]
        public IActionResult GetAllUsers() {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditStudent(int id) {
            Student student = await _context.Students.FirstOrDefaultAsync(u => u.Id == id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(int id, Student student) {
            if (id != student.Id) {
                TempData["error"] = "Student not found.";
                return RedirectToAction("GetAllStudents");
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(student);  // Update the student entity in the database
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Student updated successfully!";
                }
                catch (DbUpdateConcurrencyException) {
                    if (!_context.Students.Any(s => s.Id == student.Id)) {
                        TempData["error"] = "Student not found.";
                        return RedirectToAction("GetAllStudents");
                    }
                    else {
                        throw;
                    }
                }
            }

            return RedirectToAction("GetAllStudents");
        }

        // GET: Admin/StudentDetails/{id}
        [HttpGet]
        public async Task<IActionResult> StudentDetails(int id) {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) {
                TempData["error"] = "Student not found.";
                return RedirectToAction("GetAllStudents");
            }

            return View(student);
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id) {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
            // Logging the student ID
            _logger.LogInformation("Attempting to delete student with ID: {0}", id);

            // Find the student by ID
            Student student = await _context.Students.FindAsync(id);

            // Check if the student exists
            if (student == null) {
                // If student not found, show an error and redirect
                TempData["error"] = "Student not found.";
                return RedirectToAction("GetAllStudents");  // Redirect back to the list of students
            }

            // Remove the student from the database
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();  // Save changes to persist the deletion

            // Show a success message and redirect
            TempData["success"] = "Student deleted successfully!";
            return RedirectToAction("GetAllStudents");  // Redirect back to the list of students
        }

    }
}