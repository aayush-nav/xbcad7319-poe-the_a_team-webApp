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
    /// <summary>
    ///  Attendance controller - handles all the methods for the leave requests and the overtime requests
    ///  makes use of the TokenAuthorizationFilter for all the methods to block the routes
    /// </summary>
    public class AttendanceController : Controller
    {
        // <summary>
        /// Returns the Index View
        /// </summary>
        [TokenAuthorizationFilter]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Retrieve the pending leave requests from the database 
        /// Store the employee details and link it to the leave requests
        /// </summary>
        /// <returns>json result with the leave requests list</returns>
        [TokenAuthorizationFilter]
        // Anjali Work
        [HttpGet]
        public async Task<IActionResult> GetPendingLeaveRequests()
        {
            // Firebase connection
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

        /// <summary>
        /// method to process the leave request - either accept of reject 
        /// based on the status it sends to another database based on the status
        /// from there the user will get an email to notify them on the status
        /// </summary>
        /// <param name="request">employee details of the request</param>
        /// <returns>processed leave request</returns>
        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> ProcessLeaveRequest([FromBody] LeaveProcessRequest request)
        {
            // Firebase Conenction
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

        /// <summary>
        /// Retrieve the pending overtime requests from the database 
        /// Store the employee details and link it to the overtime requests
        /// </summary>
        /// <returns></returns>
        [TokenAuthorizationFilter]
        public async Task<IActionResult> GetOvertimeRequests()
        {
            // Firebase connection
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

        /// method to process the overtime request - either accept of reject 
        /// based on the status it sends to another database based on the status
        /// from there the user will get an email to notify them on the status
        /// </summary>
        /// <param name="request">employee details of the overtime</param>
        /// <returns>processed overtime request</returns>
        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<IActionResult> ProcessOvertimeRequest([FromBody] OvertimeProcessRequest request)
        {
            // Firebase connection
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
