namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model to handle the overtime request process
    public class OvertimeProcessRequest
    {
        public string? EmpId { get; set; }
        public string? Date { get; set; }
        public string? Status { get; set; } // "Approved" or "Declined"
    }

}
