using System.ComponentModel.DataAnnotations;

namespace IFMS_V3.Models.Burs
{
    public class BursModel
    {
        [Required(ErrorMessage = "Payee is required.")]
        [MinLength(2, ErrorMessage = "Payee must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Payee cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Payee contains invalid characters.")]
        public string Payee { get; set; }

        [Required(ErrorMessage = "BURS Number is required.")]
        [MaxLength(100, ErrorMessage = "BURS Number cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\]+$", ErrorMessage = "BURS Number can only contain letters, numbers, and dashes.")]
        public string BursNumber { get; set; }

        [Required(ErrorMessage = "Office is required.")]
        [MinLength(2, ErrorMessage = "Office must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Office cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Office contains invalid characters.")]
        public string Office { get; set; }

        [Required(ErrorMessage = "BURS Date is required.")]
        public DateTime? BursDate { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MinLength(5, ErrorMessage = "Address must be at least 5 characters long.")]
        [MaxLength(150, ErrorMessage = "Address cannot exceed 150 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Address contains invalid characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Particulars are required.")]
        [MinLength(5, ErrorMessage = "Particulars must be at least 5 characters long.")]
        [MaxLength(500, ErrorMessage = "Particulars cannot exceed 500 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Particulars contain invalid characters.")]
        public string Particulars { get; set; }

        [Required(ErrorMessage = "Responsibility Center ID is required.")]
        [Range(0, 99999, ErrorMessage = "Responsibility Center ID must be a positive integer.")]
        public int ResponsibilityCenterId { get; set; }

        [Required(ErrorMessage = "MFO/PAP ID is required.")]
        [Range(0, 99999, ErrorMessage = "MFO/PAP ID must be a positive integer.")]
        public int MfoPapId { get; set; }

        [Required(ErrorMessage = "UACS Code ID is required.")]
        [Range(0, 99999, ErrorMessage = "UACS Code ID must be a positive integer.")]
        public int UacsCodeId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must be a valid number with up to 2 decimal places.")]
        public double Amount { get; set; }
        [Required(ErrorMessage = "Signatory A is required.")]
        [MinLength(2)]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Signatory A contains invalid characters.")]
        public string SignatoryA { get; set; }

        [Required(ErrorMessage = "Signatory B is required.")]
        [MinLength(2)]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Signatory B contains invalid characters.")]
        public string SignatoryB { get; set; }

        [Required(ErrorMessage = "Position A is required.")]
        [MinLength(2)]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Position A contains invalid characters.")]
        public string PositionA { get; set; }

        [Required(ErrorMessage = "Position B is required.")]
        [MinLength(2)]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Position B contains invalid characters.")]
        public string PositionB { get; set; }

        // Optional fields - no validation applied here
        public string Fund { get; set; }
        public double Total { get; set; }

    }

    public class ResponsibilityCenter
    {
        public int id { get; set; }
        public string rcCode { get; set; }
        public string rcName { get; set; }
    }

    public class MFOPAP
    {
        public int id { get; set; }
        public long mfoPapCode { get; set; }
        public string mfoPapDescription { get; set; }
    }

    public class UACS
    {
        public int id { get; set; }
        public string subObjectCode { get; set; }
        public long uacs { get; set; }
    }

    public class BursValidationError
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
    }

}

