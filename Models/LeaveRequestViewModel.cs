namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model for the leave request - matching the field in the database
    public class LeaveRequestViewModel
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Document { get; set; }
        public int NumofDays { get; set; }
        public string Status { get; set; }
    }

}
