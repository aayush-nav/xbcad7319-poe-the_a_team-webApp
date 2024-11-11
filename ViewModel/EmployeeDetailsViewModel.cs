using Microsoft.AspNetCore.Mvc.ViewEngines;
using XBCAD7319_SparkLine_HR_WebApp.Models;

namespace XBCAD7319_SparkLine_HR_WebApp.ViewModel
{
    public class EmployeeDetailsViewModel
    {
        public Employee Employee { get; set; }
        public JobDetails JobDetails { get; set; }
        public List<AttendanceRecord> AttendanceRecords { get; set; }
        public Payroll Payroll { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
