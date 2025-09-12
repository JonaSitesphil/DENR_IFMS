using IFMS_V3.Models.Auth;

    namespace IFMS_V3.Models.Ors.BudgetOfficer
    {
        public class BudgetOfficerModel
        {
            public class BudgetOfficerDashboardApiResponse
            {
                public string Status { get; set; }
                public string Message { get; set; }
                public BudgetOfficerData Data { get; set; }
            }

            public class BudgetOfficerData
            {
                public List<BudgetOfficerDashboard> OrsList { get; set; }
                public int TotalRecords { get; set; }
            }

            public class BudgetOfficerOrsData
        {
            public List<BudgetOfficerDashboard> OrsList { get; set; }
            public int Total { get; set; }
        }

        public class BudgetOfficerBursData
        {
            public List<BudgetOfficerDashboard> Ors { get; set; }
            public int Total { get; set; }
        }

        public class BudgetOfficerDashboard
        {
           
            public int Id { get; set; }
            public string OrsNumber { get; set; }
            public DateTime? OrsDate { get; set; }
            public int FundCodeId { get; set; }
            public string fundCodeName { get; set; }

            public int FundClusterId { get; set; }
            public string fundClusterName { get; set; }

            public string Payee { get; set; }
            public string Office { get; set; }
            public string Address { get; set; }
            public string Particulars { get; set; }
            public int ResponsibilityCenterId { get; set; }
            public string responsibilityCenterName { get; set; }
            public int MfoPapId { get; set; }
            public string mfoPapName { get; set; }

            public int UacsCodeId { get; set; }
            public int uacsCodeName { get; set; }

            public double Amount { get; set; }
            public double Total { get; set; }
            public string Type { get; set; }
            public string Status { get; set; }
            public string SignatoryA { get; set; }
            public string PositionA { get; set; }
            public string SignatoryB { get; set; }
            public string PositionB { get; set; }
        }

        }
    }

