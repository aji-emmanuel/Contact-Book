using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserManagement.DTOs
{
    public class RegRequestDTO : IValidatableObject
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$",
            ErrorMessage = "Name should begin with a capital letter, followed by small letters")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$",
            ErrorMessage = "Name should begin with a capital letter, followed by small letters")]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string City { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string State { get; set; }

        [Required]
        public string Phonenumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(FirstName == LastName)
            {
                yield return new ValidationResult("FirstName should be different from LastName");
            }
            if(City == State)
            {
                yield return new ValidationResult("The provided City should be different from the State");
            }
            if(Gender!= "Male" && Gender!= "Female")
            {
                yield return new ValidationResult("Gender should be either Male or Female");
            }
        }
    }
}
