using System.ComponentModel.DataAnnotations;

namespace IFMS_V3.Dto.UpdateSignatories
{
    public class UpdateSignatoriesDto
    {
            [MinLength(2, ErrorMessage = "Printed Name must be at least 2 characters long.")]
            [MaxLength(100, ErrorMessage = "Printed Name cannot exceed 100 characters.")]
            [RegularExpression(@"^[A-Z.]+$", ErrorMessage = "Printed Name can only contain uppercase letters and periods.")]
            public string? PrintedName { get; set; }

            [MinLength(2, ErrorMessage = "Position must be at least 2 characters long.")]
            [MaxLength(100, ErrorMessage = "Position cannot exceed 100 characters.")]
            [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Position contains invalid characters.")]
            public string? Position { get; set; }
        
    }
}
