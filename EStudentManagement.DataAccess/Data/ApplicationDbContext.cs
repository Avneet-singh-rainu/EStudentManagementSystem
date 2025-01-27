using EStudentManagement.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace EStudentManagement.DataAccess.Data {

    public class ApplicationDbContext : DbContext {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        // table name = students and return type is dbset of Student.cs type from dbcontest
        public DbSet<Student> Students { get; set; }

        public DbSet<User> Users { get; set; }
    }
}