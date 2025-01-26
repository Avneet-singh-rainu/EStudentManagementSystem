using EStudentManagement.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EStudentManagement.Web.Data {

    public class ApplicationDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Seed an admin user
            modelBuilder.Entity<User>().HasData(new User {
                Id = 1,
                Username = "admin",
                Password = "admin123", // Simple password (not recommended in real-world)
                Role = "Admin"
            });
        }
    }


}
