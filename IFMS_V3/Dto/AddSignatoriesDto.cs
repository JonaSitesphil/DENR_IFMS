using System.ComponentModel.DataAnnotations;

namespace IFMS_V3.Dto.Signatories

{
    public class AddSignatoriesDto
    {
        [Required(ErrorMessage = "Printed Name is required.")]
        [MinLength(2, ErrorMessage = "Printed Name must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Printed Name cannot exceed 100 characters.")]
        [RegularExpression(@"^[A-Z .]+$", ErrorMessage = "Printed Name can only contain uppercase letters, spaces, and periods.")]
        public string PrintedName { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        [MinLength(2, ErrorMessage = "Position must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Position cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z ,]+$", ErrorMessage = "Position can only contain letters, spaces, and commas.")]
        public string Position { get; set; }
    }
}
