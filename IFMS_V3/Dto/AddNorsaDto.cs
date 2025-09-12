using System.ComponentModel.DataAnnotations;

namespace IFMS_V3.Dto.Norsa
{
    public class AddNorsaDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Responsibility Center ID must be a positive number.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Responsibility Center ID must only contain numbers.")]
        [Required(ErrorMessage = "Responsibility Center ID is required.")]
        public int? ResponsibilityCenterId { get; set; }

        [MinLength(5, ErrorMessage = "Particulars must be at least 5 characters long.")]
        [MaxLength(500, ErrorMessage = "Particulars cannot exceed 500 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Particulars contain invalid characters.")]
        [Required(ErrorMessage = "Particulars is required.")]
        public string? Particulars { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "MFO/PAP ID must be a positive integer.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "MFO/PAP ID must only contain numbers.")]
        [Required(ErrorMessage = "MFO/PAP ID is required.")]
        public int? MfoPapId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "UACS Code ID must be a positive integer.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "UACS Code ID must only contain numbers.")]
        [Required(ErrorMessage = "UACS Code ID is required.")]
        public int? UacsCodeId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must be a valid number with up to 2 decimal places.")]
        [Required(ErrorMessage = "Amount is required.")]
        public double? Amount { get; set; }

        [Required(ErrorMessage = "JEV Number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "JEV Number must be a positive number.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "JEV Number must only contain numbers.")]
        public int JevNumber { get; set; }

        [Required(ErrorMessage = "JEV Date is required.")]
        public DateTime JevDate { get; set; }

        [Required(ErrorMessage = "ORS Number is required.")]
        [MaxLength(10, ErrorMessage = "ORS Number cannot exceed 10 characters.")]
        [RegularExpression(@"^[0-9\-]+$", ErrorMessage = "ORS Number can only contain Letters.")]


        public string SerialNo { get; set; } = string.Empty;

    }
}

