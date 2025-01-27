using System.ComponentModel.DataAnnotations;

namespace EStudentManagement.Web.Models {

    public class Student {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        [RegularExpression("^[a-zA-Z]+( [a-zA-Z]+)*$", ErrorMessage = "Name must contain only letters and single spaces between words.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = "";

        public DateTime DateOfBirth { get; set; }
    }
}