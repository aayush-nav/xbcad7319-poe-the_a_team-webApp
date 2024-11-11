using Microsoft.AspNetCore.Mvc;
using XBCAD7319_SparkLine_HR_WebApp.Models;

namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Index()
        {
            // Populate data (for now, using mock data)
            var viewModel = new AttendanceViewModel
            {
                AttendanceLogs = GetAttendanceLogs(),
                AttendanceCalendar = GetAttendanceCalendar(),
                AttendanceSummary = GetAttendanceSummary(),
                LeaveRequests = GetLeaveRequests(),
                SelectedDay = null
            };

            return View(viewModel);
        }

        // AttendanceController.cs
        [HttpGet]
        public IActionResult GetCalenderView(string employeeName, int month = 0, int year = 0)
        {
            // Default to the current month and year if not provided
            if (month == 0) month = DateTime.Now.Month;
            if (year == 0) year = DateTime.Now.Year;

            // Get the first and last day of the month
            var firstDayOfMonth = new DateTime(year, month, 1);
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var lastDayOfMonth = new DateTime(year, month, daysInMonth);

            // Initialize the weeks list
            var weeks = new List<WeekViewModel>();
            var currentWeek = new WeekViewModel { Days = new List<DayViewModel>() };

            // Adjust the starting day to the beginning of the week (Monday)
            var currentDate = firstDayOfMonth.AddDays(-(int)firstDayOfMonth.DayOfWeek + 1);

            // Generate the days for the calendar view, including the previous and next months' overflow days
            while (currentDate <= lastDayOfMonth.AddDays(6 - (int)lastDayOfMonth.DayOfWeek))
            {
                var day = new DayViewModel
                {
                    Date = currentDate,
                    Status = (currentDate.Month == month) ? (currentDate.Day % 2 == 0 ? "Present" : "Absent") : "" // Mock status
                };

                currentWeek.Days.Add(day);

                // Add the week to the list when it has 7 days
                if (currentWeek.Days.Count == 7)
                {
                    weeks.Add(currentWeek);
                    currentWeek = new WeekViewModel { Days = new List<DayViewModel>() };
                }

                // Move to the next day
                currentDate = currentDate.AddDays(1);
            }

            // Create the view model
            var calendarViewModel = new AttendanceViewModel
            {
                Weeks = weeks,
                CurrentMonth = month,
                CurrentYear = year
            };

            // Pass the view model to the partial view
            return PartialView("_CalendarView", calendarViewModel);
        }

        [HttpGet]
        public IActionResult GetAttendanceSummary(DateTime date)
        {
            // Fetch the selected day's attendance details (mocked for now)
            var selectedDay = new AttendanceDay
            {
                Date = date,
                Status = "Present",
                CheckIn = "09:00",
                CheckOut = "17:00",
                HoursWorked = 8
            };

            return PartialView("_AttendanceSummary", selectedDay);
        }

        private List<AttendanceLog> GetAttendanceLogs()
        {
            return new List<AttendanceLog>
        {
            new AttendanceLog { Date = DateTime.Today, EmployeeName = "John Doe", CheckIn = "09:00", CheckOut = "17:00", Status = "Present" },
            new AttendanceLog { Date = DateTime.Today.AddDays(-1), EmployeeName = "Jame Cortny", CheckIn = "08:30", CheckOut = "15:30", Status = "Present" }
        };
        }

        private List<List<AttendanceDay>> GetAttendanceCalendar()
        {
            // Generate a calendar (for simplicity, we will just mock it here)
            var week1 = new List<AttendanceDay>
        {
            new AttendanceDay { Date = DateTime.Today.AddDays(-6), Status = "Present", CheckIn = "09:00", CheckOut = "17:00", HoursWorked = 8 },
            // ... fill the rest of the week
        };

            var week2 = new List<AttendanceDay>
        {
            new AttendanceDay { Date = DateTime.Today, Status = "Absent", CheckIn = "", CheckOut = "", HoursWorked = 0 },
            // ... fill the rest of the week
        };

            return new List<List<AttendanceDay>> { week1, week2 };
        }

        private AttendanceSummary GetAttendanceSummary()
        {
            return new AttendanceSummary { PresentDays = 18, AbsentDays = 2, LeaveDays = 1, HolidayDays = 1 };
        }

        private List<LeaveRequest> GetLeaveRequests()
        {
            return new List<LeaveRequest>
        {
            new LeaveRequest { EmployeeName = "John Doe", Date = DateTime.Today, LeaveType = "Sick Leave", Reason = "Flu", Document = "sick_note.pdf", Status = "Pending" },
            // Additional requests...
        };
        }
        public IActionResult GetWeeklyHours(string employeeName)
        {
            // Replace with real data fetching logic
            var result = new
            {
                days = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" },
                hours = new List<int> { 8, 7, 8, 5, 6, 0, 0 }
            };

            return Json(result);
        }
    }
}
