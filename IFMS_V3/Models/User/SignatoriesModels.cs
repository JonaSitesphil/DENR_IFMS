using IFMS_V3.Models.User.UserManagement;

namespace IFMS_V3.Models.User.Signatories
{
    public class SignatoriesApiResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public SignatoriesData Data { get; set; }
    }

    public class SignatoriesData
    {
        public List<SignatoriesModels> SignatoryList { get; set; }
        public int TotalRecords { get; set; }
    }

    public class SignatoriesModels
    {
        public int Id { get; set; }
        public string PrintedName { get; set; }
        public string Position { get; set; }
        public bool Disabled { get; set; }
        public int CreatedBy { get; set; }
        public string updatedByName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DisabledAt { get; set; }
    }
}

