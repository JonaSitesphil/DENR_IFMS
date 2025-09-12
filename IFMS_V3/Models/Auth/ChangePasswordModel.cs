using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace IFMS_V3.Models.Auth.ChangePassword
{
    public class ChangePasswordModel : IValidatableObject
    {
        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [MinLength(8, ErrorMessage = "New password must be at least 8 characters long")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is required")]
        [Compare("NewPassword", ErrorMessage = "New password and confirmation do not match")]
        public string ConfirmNewPassword { get; set; }

        // Custom validation for password complexity
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(NewPassword))
            {
                if (!Regex.IsMatch(NewPassword, @"[A-Z]"))
                    yield return new ValidationResult("Password must contain at least one uppercase letter", new[] { nameof(NewPassword) });

                if (!Regex.IsMatch(NewPassword, @"[a-z]"))
                    yield return new ValidationResult("Password must contain at least one lowercase letter", new[] { nameof(NewPassword) });

                if (!Regex.IsMatch(NewPassword, @"\d"))
                    yield return new ValidationResult("Password must contain at least one number", new[] { nameof(NewPassword) });

                if (!Regex.IsMatch(NewPassword, @"[\W_]"))
                    yield return new ValidationResult("Password must contain at least one special character", new[] { nameof(NewPassword) });
            }
        }
    }
}
 
