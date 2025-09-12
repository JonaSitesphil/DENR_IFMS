using System.ComponentModel.DataAnnotations;

namespace IFMS_V3.Dto.Update
{
    public class UpdateUserDto
    {
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters.")]
        [RegularExpression(@"^[a-zA-Z\s\-]+$", ErrorMessage = "First name can only contain letters, spaces, and hyphens.")]
        public string? FirstName { get; set; }
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters.")]
        [RegularExpression(@"^[a-zA-Z\s\-]+$", ErrorMessage = "Last name can only contain letters, spaces, and hyphens.")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }
        [Range(0, 9999, ErrorMessage = "Role ID must be a positive integer.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Role ID must only contain numbers.")]
        public int? RoleId { get; set; }
    }
}
