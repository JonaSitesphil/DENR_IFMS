using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using IFMS_V3.Models.Ors;

namespace IFMS_V3.Dto.Ors
{
    public class AddOrsDto
    {
        [Required(ErrorMessage = "ORS ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ORS ID must be a positive number.")]
        public int Ors_Id { get; set; }

        [Required(ErrorMessage = "ORS Number is required.")]
        [MaxLength(100, ErrorMessage = "ORS Number cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\-]+$", ErrorMessage = "ORS Number can only contain letters, numbers, and dashes.")]
        public string OrsNumber { get; set; }

        [Required(ErrorMessage = "ORS Date is required.")]
        public DateTime OrsDate { get; set; }

        [Required(ErrorMessage = "Allotment class is required.")]
        [EnumDataType(typeof(AllotmentClassEnum), ErrorMessage = "Invalid allotment class.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AllotmentClassEnum AllotmentClass { get; set; }

        [Required(ErrorMessage = "Payee is required.")]
        [MinLength(2, ErrorMessage = "Payee must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Payee cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Payee contains invalid characters.")]
        public string Payee { get; set; }

        [Required(ErrorMessage = "Office is required.")]
        [MinLength(2, ErrorMessage = "Office must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Office cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Office contains invalid characters.")]
        public string Office { get; set; }

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

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must be a valid number with up to 2 decimal places.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Signatory Box A is required.")]
        [MinLength(2, ErrorMessage = "Signatory Box A must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Signatory Box A cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Signatory Box A contains invalid characters.")]
        public string SignatoryA { get; set; }

        [Required(ErrorMessage = "Signatory Box B is required.")]
        [MinLength(2, ErrorMessage = "Signatory Box B must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Signatory Box B cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Signatory Box B contains invalid characters.")]
        public string SignatoryB { get; set; }

        [Required(ErrorMessage = "Position A is required.")]
        [MinLength(2, ErrorMessage = "Position A must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Position A cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Position A contains invalid characters.")]
        public string PositionA { get; set; }

        [Required(ErrorMessage = "Position B is required.")]
        [MinLength(2, ErrorMessage = "Position B must be at least 2 characters long.")]
        [MaxLength(100, ErrorMessage = "Position B cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Position B contains invalid characters.")]
        public string PositionB { get; set; }

        [Required(ErrorMessage = "Fund Code ID is required.")]
        [Range(0, 99999, ErrorMessage = "Fund Code ID must be a positive integer.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Fund Code ID must only contain numbers.")]
        public int FundCodeId { get; set; }

        [Required(ErrorMessage = "Total is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total must be greater than 0.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Total must be a valid number with up to 2 decimal places.")]
        public double Total { get; set; }

        [Required(ErrorMessage = "Fund Cluster ID is required.")]
        [Range(0, 99999, ErrorMessage = "Fund Cluster ID must be a positive integer.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Fund Cluster ID must only contain numbers.")]
        public int FundClusterId { get; set; }

        [Required(ErrorMessage = "Responsibility Center ID is required.")]
        [Range(0, 99999, ErrorMessage = "Responsibility Center ID must be a positive integer.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Responsibility Center ID must only contain numbers.")]
        public int ResponsibilityCenterId { get; set; }

        [Required(ErrorMessage = "Uacs Code ID is required.")]
        [Range(0, 99999, ErrorMessage = "Uacs Code ID must be a positive integer.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Uacs Code ID must only contain numbers.")]
        public int UacsCodeId { get; set; }

        [Required(ErrorMessage = "MFO/PAP ID is required.")]
        [Range(0, 99999, ErrorMessage = "MFO/PAP ID must be a positive integer.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "MFO/PAP ID must only contain numbers.")]
        public int MfoPapId { get; set; }
    }
}
