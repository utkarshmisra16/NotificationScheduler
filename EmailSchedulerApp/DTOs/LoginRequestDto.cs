using System.ComponentModel.DataAnnotations;

namespace EmailSchedulerApp.DTOs
{
    public class LoginRequestDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class RegisterRequestDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Range(typeof(bool), "true", "true",
            ErrorMessage = "You must accept the terms.")]
        public bool AcceptTerms { get; set; }
    }
}