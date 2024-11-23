using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using XBCAD7319_SparkLine_HR_WebApp.Models;
using XBCAD7319_SparkLine_HR_WebApp.Token;
using XBCAD7319_SparkLine_HR_WebApp.ViewModel;

namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    public class AttendanceController : Controller
    {
        [TokenAuthorizationFilter]
        public IActionResult Index()
        {
            
            return View();
        }

        //[TokenAuthorizationFilter]
        //// AttendanceController.cs
        //[HttpGet]
        //public IActionResult GetCalenderView(string employeeName, int month = 0, int year = 0)
        //{
        //    // Default to the current month and year if not provided
        //    if (month == 0) month = DateTime.Now.Month;
        //    if (year == 0) year = DateTime.Now.Year;

        //    // Get the first and last day of the month
        //    var firstDayOfMonth = new DateTime(year, month, 1);
        //    var daysInMonth = DateTime.DaysInMonth(year, month);
        //    var lastDayOfMonth = new DateTime(year, month, daysInMonth);

        //    // Initialize the weeks list
        //    var weeks = new List<WeekViewModel>();
        //    var currentWeek = new WeekViewModel { Days = new List<DayViewModel>() };

        //    // Adjust the starting day to the beginning of the week (Monday)
        //    var currentDate = firstDayOfMonth.AddDays(-(int)firstDayOfMonth.DayOfWeek + 1);

        //    // Generate the days for the calendar view, including the previous and next months' overflow days
        //    while (currentDate <= lastDayOfMonth.AddDays(6 - (int)lastDayOfMonth.DayOfWeek))
        //    {
        //        var day = new DayViewModel
        //        {
        //            Date = currentDate,
        //            Status = (currentDate.Month == month) ? (currentDate.Day % 2 == 0 ? "Present" : "Absent") : "" // Mock status
        //        };

        //        currentWeek.Days.Add(day);

        //        // Add the week to the list when it has 7 days
        //        if (currentWeek.Days.Count == 7)
        //        {
        //            weeks.Add(currentWeek);
        //            currentWeek = new WeekViewModel { Days = new List<DayViewModel>() };
        //        }

        //        // Move to the next day
        //        currentDate = currentDate.AddDays(1);
        //    }

        //    // Create the view model
        //    var calendarViewModel = new AttendanceViewModel
        //    {
        //        Weeks = weeks,
        //        CurrentMonth = month,
        //        CurrentYear = year
        //    };

        //    // Pass the view model to the partial view
        //    return PartialView("_CalendarView", calendarViewModel);
        //}

        //[TokenAuthorizationFilter]
        //[HttpGet]
        //public IActionResult GetAttendanceSummary(DateTime date)
        //{
        //    // Fetch the selected day's attendance details (mocked for now)
        //    var selectedDay = new AttendanceDay
        //    {
        //        Date = date,
        //        Status = "Present",
        //        CheckIn = "09:00",
        //        CheckOut = "17:00",
        //        HoursWorked = 8
        //    };

        //    return PartialView("_AttendanceSummary", selectedDay);
        //}


        //private List<AttendanceLog> GetAttendanceLogs()
        //{
        //    return new List<AttendanceLog>
        //{
        //    new AttendanceLog { Date = DateTime.Today, EmployeeName = "John Doe", CheckIn = "09:00", CheckOut = "17:00", Status = "Present" },
        //    new AttendanceLog { Date = DateTime.Today.AddDays(-1), EmployeeName = "Jame Cortny", CheckIn = "08:30", CheckOut = "15:30", Status = "Present" }
        //};
        //}

        //private List<List<AttendanceDay>> GetAttendanceCalendar()
        //{
        //    // Generate a calendar (for simplicity, we will just mock it here)
        //    var week1 = new List<AttendanceDay>
        //{
        //    new AttendanceDay { Date = DateTime.Today.AddDays(-6), Status = "Present", CheckIn = "09:00", CheckOut = "17:00", HoursWorked = 8 },
        //    // ... fill the rest of the week
        //};

        //    var week2 = new List<AttendanceDay>
        //{
        //    new AttendanceDay { Date = DateTime.Today, Status = "Absent", CheckIn = "", CheckOut = "", HoursWorked = 0 },
        //    // ... fill the rest of the week
        //};

        //    return new List<List<AttendanceDay>> { week1, week2 };
        //}

        //private AttendanceSummary GetAttendanceSummary()
        //{
        //    return new AttendanceSummary { PresentDays = 18, AbsentDays = 2, LeaveDays = 1, HolidayDays = 1 };
        //}

        //private List<LeaveRequest> GetLeaveRequests()
        //{
        //    return new List<LeaveRequest>
        //{
        //    new LeaveRequest { EmployeeName = "John Doe", Date = DateTime.Today, LeaveType = "Sick Leave", Reason = "Flu", Document = "sick_note.pdf", Status = "Pending" },
        //    // Additional requests...
        //};
        //}
        //public IActionResult GetWeeklyHours(string employeeName)
        //{
        //    // Replace with real data fetching logic
        //    var result = new
        //    {
        //        days = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" },
        //        hours = new List<int> { 8, 7, 8, 5, 6, 0, 0 }
        //    };

        //    return Json(result);
        //}


        [TokenAuthorizationFilter]
        // Anjali Work
        [HttpGet]
        public async Task<IActionResult> GetPendingLeaveRequests()
        {
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");
            var pendingLeaves = await firebase
                .Child("SparkLineHR")
                .Child("Pending Leave Requests")
                .OnceAsync<LeaveRequestViewModel>();

            var leaveRequests = new List<LeaveRequestViewModel>();

            foreach (var leave in pendingLeaves)
            {
                var parts = leave.Key.Split(',');
                var empId = parts[0];
                var fromDate = DateTime.Parse(parts[1]);

                // Fetch employee details
                var employee = await firebase
                    .Child("SparkLineHR")
                    .Child("employees_sparkline")
                    .Child(empId)
                    .OnceSingleAsync<EmployeeDetailsViewModel>();

                leaveRequests.Add(new LeaveRequestViewModel
                {
                    EmployeeId = empId,
                    EmployeeName = employee.Employee.Name,
                    FromDate = leave.Object.FromDate,
                    ToDate = leave.Object.ToDate,
                    LeaveType = leave.Object.LeaveType,
                    Document = leave.Object.Document,
                    Status = "Pending", // All fetched records are pending
                    NumofDays = (DateTime.Parse(leave.Object.ToDate) - DateTime.Parse(leave.Object.FromDate)).Days,
                });
            }

            return Json(new { success = true, data = leaveRequests });
        }

        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> ProcessLeaveRequest([FromBody] LeaveProcessRequest request)
        {
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");

            // Construct the key for the pending leave request
            var leaveKey = $"{request.EmpId},{request.FromDate}";

            try
            {
                // Fetch the pending leave request
                var pendingLeave = await firebase
                    .Child("SparkLineHR")
                    .Child("Pending Leave Requests")
                    .Child(leaveKey)
                    .OnceSingleAsync<LeaveRequestViewModel>();

                if (pendingLeave == null)
                {
                    return Json(new { success = false, message = "Leave request not found." });
                }

                // Create the processed leave request with updated status
                var processedLeave = new
                {
                    pendingLeave.Document,
                    pendingLeave.FromDate,
                    pendingLeave.ToDate,
                    pendingLeave.LeaveType,
                    status = request.Status, // Approved or Declined
                    processedDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    empID = request.EmpId
                };

                // Save the processed leave request to the appropriate node
                var statusNode = request.Status.ToLower() == "approved" ? "Approved Leave Requests" : "Declined Leave Requests";

                await firebase
                    .Child("SparkLineHR")
                    .Child(statusNode)
                    .Child(leaveKey)
                    .PutAsync(processedLeave);

                // Remove the pending leave request
                await firebase
                    .Child("SparkLineHR")
                    .Child("Pending Leave Requests")
                    .Child(leaveKey)
                    .DeleteAsync();

                // Fetch employee details
                var employee = await firebase
                    .Child("SparkLineHR")
                    .Child("employees_sparkline")
                    .Child(request.EmpId)
                    .OnceSingleAsync<EmployeeDetailsViewModel>();

                if (employee == null)
                {
                    return Json(new { success = false, message = "Employee details not found." });
                }

                // Send email notification
                var emailPayload = new
                {
                    email = employee.Employee.Email,
                    name = employee.Employee.Name,
                    subjectType = "leave",
                    status = request.Status.ToLower(),
                    date = pendingLeave.FromDate
                };

                using (var client = new HttpClient())
                {
                    var emailResponse = await client.PostAsJsonAsync("https://email-sparklineapi-a-team.onrender.com/send-email", emailPayload);

                    if (!emailResponse.IsSuccessStatusCode)
                    {
                        return Json(new { success = false, message = "Leave processed but email notification failed." });
                    }
                }

                return Json(new { success = true, message = $"Leave request {request.Status.ToLower()} successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while processing the leave request.", error = ex.Message });
            }
        }


        [TokenAuthorizationFilter]
        public async Task<IActionResult> GetOvertimeRequests()
        {
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");

            try
            {
                // Fetch all overtime requests
                var overtimeRequests = await firebase
                    .Child("SparkLineHR")
                    .Child("Overtime Requests")
                    .OnceAsync<OvertimeRequestViewModel>();

                // Map Firebase data to a view model
                var overtimeRequestsList = overtimeRequests.Select(req => new OvertimeRequestViewModel
                {
                    EmpId = req.Key.Split(',')[0],
                    Date = req.Key.Split(',')[1],
                    EmployeeName = "Loading...", // Placeholder, fetch employee details later
                    FriHours = req.Object.FriHours,
                    MonHours = req.Object.MonHours,
                    TueHours = req.Object.TueHours,
                    WedHours = req.Object.WedHours,
                    ThuHours = req.Object.ThuHours,
                    TotalHours = req.Object.FriHours + req.Object.MonHours +
                                 req.Object.TueHours + req.Object.WedHours +
                                 req.Object.ThuHours,
                    Status = "Pending"
                }).ToList();

                // Fetch employee names for each overtime request
                foreach (var req in overtimeRequestsList)
                {
                    var employee = await firebase
                        .Child("SparkLineHR")
                        .Child("employees_sparkline")
                        .Child(req.EmpId)
                        .OnceSingleAsync<EmployeeDetailsViewModel>();

                    req.EmployeeName = employee?.Employee?.Name ?? "Unknown";
                }

                return Json(new { success = true, data = overtimeRequestsList });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to fetch overtime requests.", error = ex.Message });
            }
        }

        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> ProcessOvertimeRequest([FromBody] OvertimeProcessRequest request)
        {
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");

            // Construct the key for the overtime request
            var overtimeKey = $"{request.EmpId},{request.Date}";

            try
            {
                // Fetch the overtime request
                var overtimeRequest = await firebase
                    .Child("SparkLineHR")
                    .Child("Overtime Requests")
                    .Child(overtimeKey)
                    .OnceSingleAsync<OvertimeRequestViewModel>();

                if (overtimeRequest == null)
                {
                    return Json(new { success = false, message = "Overtime request not found." });
                }

                // Calculate total hours worked (sum up all the weekday hours)
                var totalHours = overtimeRequest.FriHours + overtimeRequest.MonHours +
                                 overtimeRequest.TueHours + overtimeRequest.WedHours +
                                 overtimeRequest.ThuHours;

                // Prepare the processed overtime request
                var processedOvertime = new
                {
                    overtimeRequest.FriHours,
                    overtimeRequest.MonHours,
                    overtimeRequest.TueHours,
                    overtimeRequest.WedHours,
                    overtimeRequest.ThuHours,
                    totalHours,
                    status = request.Status, // Approved or Declined
                    processedDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    empID = request.EmpId
                };

                // Save the processed overtime request to the appropriate node
                var statusNode = request.Status.ToLower() == "approved" ? "Approved Overtime" : "Declined Overtime";

                await firebase
                    .Child("SparkLineHR")
                    .Child(statusNode)
                    .Child(overtimeKey)
                    .PutAsync(processedOvertime);

                // Remove the overtime request from the pending node
                await firebase
                    .Child("SparkLineHR")
                    .Child("Overtime Requests")
                    .Child(overtimeKey)
                    .DeleteAsync();

                // Fetch employee details
                var employee = await firebase
                    .Child("SparkLineHR")
                    .Child("employees_sparkline")
                    .Child(request.EmpId)
                    .OnceSingleAsync<EmployeeDetailsViewModel>();

                if (employee == null)
                {
                    return Json(new { success = false, message = "Employee details not found." });
                }

                // Send email notification
                var emailPayload = new
                {
                    email = employee.Employee.Email,
                    name = employee.Employee.Name,
                    subjectType = "overtime",
                    status = request.Status.ToLower(),
                    date = request.Date
                };

                using (var client = new HttpClient())
                {
                    var emailResponse = await client.PostAsJsonAsync("https://email-sparklineapi-a-team.onrender.com/send-email", emailPayload);

                    if (!emailResponse.IsSuccessStatusCode)
                    {
                        return Json(new { success = false, message = "Overtime processed but email notification failed." });
                    }
                }

                return Json(new { success = true, message = $"Overtime request {request.Status.ToLower()} successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while processing the overtime request.", error = ex.Message });
            }
        }






    }
}
