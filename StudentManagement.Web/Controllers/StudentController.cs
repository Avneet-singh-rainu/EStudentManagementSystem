using EStudentManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Web.Data;

namespace EStudentManagement.Web.Controllers {

    public class StudentController : Controller {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext _db) {
            _context = _db;
        }

        public IActionResult Index() {
            var students = _context.Students.ToList();
            return View(students);
        }

        // GET: Admin/EditDetails/{id}
        //[HttpGet]
        //public async Task<IActionResult> EditStudent(int id) {
        //    Console.WriteLine(id + "......................");
        //    Student student = await _context.Students.FindAsync(id);

        //    if (student == null) {
        //        TempData["error"] = "Student not found.";
        //        return RedirectToAction("Index", "Student");
        //    }

        //    return View(student);
        //}

        [HttpGet]
        public async Task<IActionResult> EditStudent(int id) {
            var student = await _context.Students.FindAsync(id);

            // Access session within the context of a non-static controller method
            var loggedInUsername = HttpContext.Session.GetString("Username");
            Console.WriteLine(loggedInUsername + ".......................................");

            if (student == null) {
                TempData["error"] = "Student not found.";
                return RedirectToAction("Index", "Student");
            }

            // Check if the logged-in user is the same as the student being edited
            if (student.Name != loggedInUsername) {
                TempData["error"] = "You are not authorized to edit this student's details.";
                return RedirectToAction("Index", "Student");
            }

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(int id, Student student) {
            if (id != student.Id) {
                TempData["error"] = "Student not found.";
                return RedirectToAction("Index");
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
                        return RedirectToAction("Index");
                    }
                    else {
                        throw;
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}