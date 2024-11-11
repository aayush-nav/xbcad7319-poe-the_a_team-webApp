using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using XBCAD7319_SparkLine_HR_WebApp.Models;
using XBCAD7319_SparkLine_HR_WebApp.ViewModel;


namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Details(int id)
        {
            // Mock Employee data
            var employee = new Employee
            {
                Name = "Jake Smith",
                Department = "IT",
                Contact = "+27789293745",
                Email = "jakesmith@example.com",
                Address = "123 Main st, city, country",
                DateOfBirth = new DateTime(1990, 1, 1)
            };

            // Mock Attendance Records
            var attendanceRecords = new List<AttendanceRecord>
            {
                new AttendanceRecord { Id = 1, Date = DateTime.Now.AddDays(-2), CheckIn = new TimeSpan(9, 0, 0), CheckOut = new TimeSpan(17, 0, 0), Status = "Present" },
                new AttendanceRecord { Id = 2, Date = DateTime.Now.AddDays(-1), CheckIn = new TimeSpan(9, 10, 0), CheckOut = new TimeSpan(16, 50, 0), Status = "Present" }
            };

            // Mock Job Details
            var jobDetails = new JobDetails
            {
                JobTitle = "Senior Developer",
                Department = "IT",
                Manager = "Jane Steve",
                EmploymentType = "Full-time",
                HireDate = new DateTime(2020, 1, 1),
                Status = "Active",
                JobDescription = "Responsible for software development and managing the team.",

                // Populate Dropdown Options
                RoleOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Developer", Text = "Developer" },
                    new SelectListItem { Value = "Manager", Text = "Manager" },
                    new SelectListItem { Value = "Analyst", Text = "Analyst" }
                },
                DepartmentOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "HR", Text = "HR" },
                    new SelectListItem { Value = "IT", Text = "IT" },
                    new SelectListItem { Value = "Finance", Text = "Finance" }
                },
                EmployeeTypeOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "FullTime", Text = "Full-Time" },
                    new SelectListItem { Value = "PartTime", Text = "Part-Time" },
                    new SelectListItem { Value = "Contract", Text = "Contract" }
                },
                StatusOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Active", Text = "Active" },
                    new SelectListItem { Value = "Inactive", Text = "Inactive" },
                    new SelectListItem { Value = "OnLeave", Text = "On Leave" }
                }
            };

            // Mock Reviews
            var reviews = new List<Review>
            {
                new Review { Id = 1, ReviewDate = new DateTime(2024, 8, 1), Rating = "Excellent", Reviewer = "Jane Steve", Feedback = "Smith has shown exceptional performance" }
            };

            // Mock Payroll
            var payroll = new Payroll
            {
                Salary = 70000,
                Bonus = 5000,
                Deductions = 2000,
                GrossPay = 75000,                // Example Gross Pay
                NetPay = 68000,                  // Example Net Pay
                PaymentDate = DateTime.Now,      // Example Payment Date
                PaymentMethod = "EFT",           // Example Payment Method
                Bank = "ABC Bank",               // Example Bank
                BankAccountNumber = "12345678",  // Example Bank Account Number
                AccountType = "Savings",         // Example Account Type
                BranchCode = "000123"            // Example Branch Code
            };

            // Create ViewModel
            var viewModel = new EmployeeDetailsViewModel
            {
                Employee = employee,
                JobDetails = jobDetails,
                AttendanceRecords = attendanceRecords,
                Payroll = payroll,
                Reviews = reviews
            };

            return View(viewModel);
        }
        public IActionResult Management()
        {
            return View();
        }

        public IActionResult Onboarding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveEmployeeDetails(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                // Save employee details logic here
                return RedirectToAction("EmployeeMaster");
            }
            return View("EmployeeMaster", model);
        }

        [HttpGet]
        public ActionResult PayrollInfo()
        {
            // Load employee details or return a blank form
            return View(new EmployeeModel());
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
