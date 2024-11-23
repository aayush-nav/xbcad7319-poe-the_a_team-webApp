using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using XBCAD7319_SparkLine_HR_WebApp.Models;
using XBCAD7319_SparkLine_HR_WebApp.Token;

namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    public class AnalyticsController : Controller
    {
        [TokenAuthorizationFilter]
        public IActionResult Index()
        {
            return View();
        }

        [TokenAuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> GetYearlyEmployeeCount()
        {
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");
            try
            {
                var employees = await firebase
                    .Child("SparkLineHR")
                    .Child("employees_sparkline")
                    .OnceAsync<EmployeeDetailsViewModelAllFour>();

                // Get the current date and calculate the date 12 months ago
                DateTime currentDate = DateTime.UtcNow;
                DateTime oneYearAgo = currentDate.AddMonths(-12);

                var employeeCountByMonth = employees
                    .Where(emp => emp.Object.jobdetails.HireDate >= DateTime.Now.AddMonths(-12))
                    .GroupBy(emp => emp.Object.jobdetails.HireDate.ToString("yyyy-MM")) // Use "yyyy-MM-dd"
                    .ToDictionary(group => group.Key, group => group.Count());

                return Json(new { success = true, data = employeeCountByMonth });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [TokenAuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> GetPerformanceReviews(string employeeId)
        {
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");

            try
            {
                var reviews = await firebase
                    .Child("SparkLineHR")
                    .Child("performanceReviews")
                    .OnceAsync<PerformanceReview>();

                var employeid = employeeId;

                var filteredReviews = reviews
                .Where(review => review.Object.EmployeeNumber == employeeId)
                .Select(review => new
                {
                    ReviewDate = review.Object.ReviewDate,
                    PerformanceRating = review.Object.PerformanceRating,
                    Feedback = review.Object.Feedback
                })
                .OrderBy(r => r.ReviewDate)
                .ToList();

                return Json(new { success = true, data = filteredReviews });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");

            try
            {
                var employees = await firebase
                    .Child("SparkLineHR")
                    .Child("employees_sparkline")
                    .OnceAsync<EmployeeDetailsViewModelAllFour>();

                var employeeList = employees.Select(emp => new
                {
                    emp.Key,
                    Name = emp.Object.employee.Name
                });

                return Json(new { success = true, data = employeeList });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



    }
}
