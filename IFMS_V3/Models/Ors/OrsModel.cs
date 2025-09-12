using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IFMS_V3.Models.Ors
{
    public class OrsModel
    {
        [JsonPropertyName("Ors_Id")]
        [Required(ErrorMessage = "ORS ID is required.")]
        [Range(10, int.MaxValue, ErrorMessage = "ORS ID must be at least 10.")]
        public int Ors_Id { get; set; }


        [Required(ErrorMessage = "ORS Number is required.")]
        [MaxLength(10, ErrorMessage = "ORS Number cannot exceed 10 characters.")]
        [RegularExpression(@"^[0-9\-]+$", ErrorMessage = "ORS Number can only contain Letters.")]
        public string OrsNumber { get; set; }

        [Required(ErrorMessage = "ORS Date is required.")]
        public DateTime? OrsDate { get; set; }   // Nullable

        [Required(ErrorMessage = "Allotment class is required.")]
        public AllotmentClassEnum AllotmentClass { get; set; }

        [Required(ErrorMessage = "Fund Code is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Fund Code.")]
        public int FundCodeId { get; set; }

        [Required(ErrorMessage = "Fund Cluster is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Fund Cluster.")]
        public int FundClusterId { get; set; }
        public FundCluster FundCluster { get; set; }   // navigation property

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

        [Required(ErrorMessage = "Responsibility Center is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Responsibility Center.")]
        public int ResponsibilityCenterId { get; set; }
        public ResponsibilityCenter ResponsibilityCenter { get; set; }
     



        [Required(ErrorMessage = "MFO/PAP is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid MFO/PAP.")]
        public int MfoPapId { get; set; }
        public MFOPAP MFOPAP { get; set; }


        [Required(ErrorMessage = "UACS Code is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid UACS Code.")]
        public int UacsCodeId { get; set; }
        public UACS UacsCode { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Total amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total must be greater than 0.")]
        public double Total { get; set; }

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
        public string fundCodeName { get; set; }
        public string fundClusterName { get; set; }
        public string responsibilityCenterName { get; set; }
        public int uacsCodeName { get; set; }
        public string mfoPapName { get; set; }


    }

    public class OrsValidationError
    {
        public Dictionary<string, List<string>> Errors { get; set; }
    }

    public class ResponsibilityCenter
    {
        public int id { get; set; }
        public string rcCode { get; set; }
        public string rcName { get; set; }
    }

    public class FundCluster
    {
        public int id { get; set; }
        public string fundClusterName { get; set; }
        public int uacs { get; set; }
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

    public class FundCode
    {
        public int id { get; set; }
        public long fundCodeNumber { get; set; }
        public string fundCodeDescription { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AllotmentClassEnum
    {
        PersonnelServices = 1,
        MaintenanceAndOtherOperatingExpenses = 2,
        FinancialExpenses = 3,
        DirectCosts = 4,
        CapitalOutlay = 5
    }
}

