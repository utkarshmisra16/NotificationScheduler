using System.ComponentModel.DataAnnotations;

namespace EmailSchedulerApp.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your new password.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your new password.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}