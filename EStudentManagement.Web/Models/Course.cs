using System.ComponentModel.DataAnnotations;
namespace EStudentManagement.Web.Models {

    public class Course {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }

}
