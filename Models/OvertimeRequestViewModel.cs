namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model for the overtime request - matching the field in the database
    public class OvertimeRequestViewModel
    {
        public string EmpId { get; set; }
        public string Date { get; set; }
        public string EmployeeName { get; set; }
        public double FriHours { get; set; }
        public double MonHours { get; set; }
        public double TueHours { get; set; }
        public double WedHours { get; set; }
        public double ThuHours { get; set; }
        public double TotalHours { get; set; }
        public string Status { get; set; }
    }

}
