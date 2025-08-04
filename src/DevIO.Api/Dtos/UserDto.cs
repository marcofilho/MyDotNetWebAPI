using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.Dtos
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "The field {0} must be a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginDto
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "The field {0} must be a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
