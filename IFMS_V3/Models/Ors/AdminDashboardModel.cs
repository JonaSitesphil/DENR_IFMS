using System.Text.Json.Serialization;

namespace IFMS_V3.Models.Ors.AdminDashboardModel
{
    public class AdminDashboardApiResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public AdminDashboardData Data { get; set; }
    }


    public class AdminDashboardData
    {
        public List<AdmindashboardModel> Records { get; set; }
        public int Total { get; set; }
    }

    //public class OrsData
    //{
    //    public List<AdmindashboardModel> records { get; set; }
    //    public int Total { get; set; }
    //}

    //public class BursData
    //{
    //    public List<AdmindashboardModel> records { get; set; }
    //    public int Total { get; set; }
    //}

    public class AdmindashboardModel
    {
        public int id { get; set; }
        public string payee { get; set; }
        public double amount { get; set; }
        public string type { get; set; }    // ✅ changed to string
        public string status { get; set; }  // ✅ changed to string
    }
}
