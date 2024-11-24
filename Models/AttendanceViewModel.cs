namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model for the Attendace View
    public class AttendanceViewModel
    {
        public List<WeekViewModel> Weeks { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        public List<AttendanceLog> AttendanceLogs { get; set; }
        public List<List<AttendanceDay>> AttendanceCalendar { get; set; } // Calendar: List of weeks, each with days
        public AttendanceSummary AttendanceSummary { get; set; }
        public AttendanceDay SelectedDay { get; set; } // Day clicked from the calendar
        public List<LeaveRequest> LeaveRequests { get; set; }

        // Aslam Updated
        // Add this property for Overtime Requests
        public List<OvertimeRequest> OvertimeRequests { get; set; } = new List<OvertimeRequest>();
    }

    // Aslam Updated - Model for the Overtime Requests
    public class OvertimeRequest
    {
        public string EmployeeName { get; set; }
        public double HoursWorked { get; set; }
        public string Status { get; set; } // e.g., "Pending", "Approved", "Rejected"
    }

    // Model for the Attendance Logs
    public class AttendanceLog
    {
        public DateTime Date { get; set; }
        public string EmployeeName { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Status { get; set; } // Present, Absent, Leave, etc.
    }

    // Model for the Attendace Day
    public class AttendanceDay
    {
        public DateTime Date { get; set; }
        public string Status { get; set; } // Present, Absent, Leave
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public int HoursWorked { get; set; }
    }

    // Model for the  Attendance Summanry
    public class AttendanceSummary
    {
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int LeaveDays { get; set; }
        public int HolidayDays { get; set; }
    }

    // Model for the LeaveRequest 
    public class LeaveRequest
    {
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
        public string LeaveType { get; set; }
        public string Reason { get; set; }
        public string Document { get; set; } // Link to the leave document
        public string Status { get; set; }
    }
}
