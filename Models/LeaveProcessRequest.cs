namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model for the leave request process to update the status 
    public class LeaveProcessRequest
    {
        public string EmpId { get; set; }
        public string FromDate { get; set; }
        public string Status { get; set; }
    }
}
