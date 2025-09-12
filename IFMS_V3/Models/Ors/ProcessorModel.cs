namespace IFMS_V3.Models.Ors.ProcessorModel
{
    public class ProcessorModel
    {
        public class ProcessorDashboardApiResponse
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public ProcessorData Data { get; set; }
        }

        public class ProcessorData
        {
            // ✅ Match "orsList" from JSON
            public List<ProcessorDashboardModel> OrsList { get; set; }

            // ✅ Match "totalRecords" from JSON
            public int TotalRecords { get; set; }
        }
        public class OrsData
        {
            public List<ProcessorDashboardModel> orsList { get; set; }
            public int Total { get; set; }
        }

        public class BursData
        {
            public List<ProcessorDashboardModel> Ors { get; set; }
            public int Total { get; set; }
        }

        public class ProcessorDashboardModel
        {
            public int Id { get; set; }
            public string? OrsNumber { get; set; }
            public DateTime? OrsDate { get; set; }
            public string? AllotmentClass { get; set; }
            public int FundCodeId { get; set; }
            public string fundCodeName { get; set; }
            public int FundClusterId { get; set; }

            public string fundClusterName { get; set; }
            public string? Payee { get; set; }
            public string? Office { get; set; }
            public string? Address { get; set; }
            public string? Particulars { get; set; }
            public string responsibilityCenterName { get; set; }

            public int responsibilityCenterId { get; set; }

            public int MfoPapId { get; set; }

            public string mfoPapName { get; set; }
            public int UacsCodeId { get; set; }

            public int uacsCodeName { get; set; }
            public double Amount { get; set; }
            public double Total { get; set; }
            public string? Type { get; set; }
            public string? SignatoryA { get; set; }
            public string? SignatoryB { get; set; }
            public string? PositionA { get; set; }
            public string? PositionB { get; set; }
            public string? Status { get; set; }
            public string? ReviewedBy { get; set; }
        }
    }
}
