using System.ComponentModel.DataAnnotations;
namespace EStudentManagement.Web.Models {

    public class Student {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }


}
