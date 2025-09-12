using System.ComponentModel.DataAnnotations;

namespace IFMS_V3.Models.Norsa
{
    public class NorsaModel
    {

        [Required(ErrorMessage = "Norsa Number is required.")]
        [MaxLength(10, ErrorMessage = "Norsa Number cannot exceed 10 characters.")]
        [RegularExpression(@"^[0-9\-]+$", ErrorMessage = "ORS Number can only contain Letters.")]
        public string SerialNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Norsa Date is required.")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Responsibility Center is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Responsibility Center.")]
        public int? ResponsibilityCenterId { get; set; }

        [Required(ErrorMessage = "Particulars are required.")]
        [MinLength(5, ErrorMessage = "Particulars must be at least 5 characters long.")]
        [MaxLength(500, ErrorMessage = "Particulars cannot exceed 500 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s.,'`-]+$", ErrorMessage = "Particulars contain invalid characters.")]
        public string Particulars { get; set; }


        [Required(ErrorMessage = "Fund Cluster is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid Fund Cluster.")]
        public int FundClusterId { get; set; }


        [Required(ErrorMessage = "MFO/PAP is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a valid MFO/PAP.")]
        public int MfoPapId { get; set; }


        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "JEV Number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "JEV Number must be a positive number.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "JEV Number must only contain numbers.")]
        public int JevNumber { get; set; }

        [Required(ErrorMessage = "JEV Date is required.")]
        public DateTime JevDate { get; set; } = DateTime.Today;  // give it a default

        [Range(1, int.MaxValue, ErrorMessage = "UACS Code ID must be a positive integer.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "UACS Code ID must only contain numbers.")]
        public int? UacsCodeId { get; set; }



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
        public class FundCode
        {
            public int id { get; set; }
            public long fundCodeNumber { get; set; }
            public string fundCodeDescription { get; set; }
        }
        public class UACS
        {
            public int id { get; set; }
            public string subObjectCode { get; set; }
            public long uacs { get; set; }
        }

            public class NorsaValidationError
            {
                public Dictionary<string, string[]> Errors { get; set; } = new();
            }
        }
    }







