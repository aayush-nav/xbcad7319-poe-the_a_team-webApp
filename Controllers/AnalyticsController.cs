using Microsoft.AspNetCore.Mvc;

namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    public class AnalyticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetYearlyEmployeeData(int year)
        {
            // Mock data
            var months = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var employeeCounts = new[] { 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105 };

            return Json(new { months, employeeCounts });
        }

        [HttpGet]
        public IActionResult GetPerformanceReviews(int employeeId)
        {
            // Mock data for the selected employee
            var reviewData = new
            {
                dates = new[] { "2023-01-15", "2023-02-20", "2023-03-25", "2023-10-18", "2023-12-25" },
                ratings = new[] { 1, 3, 4, 2, 3 } // Needs Improvement, Good, Excellent
            };

            return Json(reviewData);
        }
    }
}
