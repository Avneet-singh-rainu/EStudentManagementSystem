using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EStudentManagement.Web.Models {

    public class User {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } // Plain text password (not recommended for production)

        [Required]
        public string Role { get; set; } // "Admin" or "Student"

        public int? StudentId { get; set; } // Nullable for admin users
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }

}
