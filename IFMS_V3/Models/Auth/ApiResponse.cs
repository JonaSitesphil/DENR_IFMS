namespace IFMS_V3.Models.Auth
{
    public class ApiResponse <T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public bool FirstTime { get; set; }  

        public T Data { get; set; }
    }
}
