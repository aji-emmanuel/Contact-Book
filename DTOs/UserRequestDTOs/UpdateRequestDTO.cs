using System.ComponentModel.DataAnnotations;

namespace UserManagement.DTOs
{
    public class UpdateRequestDTO
    {
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$",
             ErrorMessage = "Name should begin with a capital letter, followed by small letters")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]{1}[a-z]{2,}$",
        ErrorMessage = "Name should begin with a capital letter, followed by small letters")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string StreetAddress { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string State { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        [RegularExpression(@"^[a-zA-Z0-9_+-]$",
        ErrorMessage = "UserName should contain only Upper case, lower case, numbers and underscore characters")]
        public string UserName { get; set; }
    }
}
