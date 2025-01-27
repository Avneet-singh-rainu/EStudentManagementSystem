using System.ComponentModel.DataAnnotations;

namespace EStudentManagement.Web.Filters {

    public class DateOfBirthValidationAttribute : ValidationAttribute {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (value is DateTime dateOfBirth) {
                if (dateOfBirth < new DateTime(1900, 1, 1) || dateOfBirth > DateTime.Now) {
                    return new ValidationResult("Date of Birth must be between 01/01/1900 and today.");
                }
            }
            return ValidationResult.Success;
        }
    }
}