using System.ComponentModel.DataAnnotations;

namespace IFMS_V3.Models.Auth.Resetpassword
{
    public class ResetPasswordModel
    {
     

        [Required(ErrorMessage = "OTP Code is required.")]
        public string OtpCode { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [MinLength(8, ErrorMessage = "New password must be at least 8 characters.")]
        [MaxLength(50, ErrorMessage = "New password cannot exceed 50 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{8,}$",
            ErrorMessage = "Password must include uppercase, lowercase, special character, and be 8+ characters long.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is required.")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
