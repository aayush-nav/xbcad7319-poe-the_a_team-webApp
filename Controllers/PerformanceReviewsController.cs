using Microsoft.AspNetCore.Mvc;
using XBCAD7319_SparkLine_HR_WebApp.Models;

namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    public class PerformanceReviewsController : Controller
    {
        // Static list to hold past training records
        private static List<Training> Trainings = new List<Training>();

        // Display the Create Review form with past reviews and past trainings
        public IActionResult CreateReview(string searchTerm = "")
        {
            var model = new PerformanceReview
            {
                ReviewDate = DateTime.Now
            };

            // Filter past reviews based on the search term
            XBCAD7319_SparkLine_HR_WebApp.Models.PerformanceReview.PastReviews = string.IsNullOrEmpty(searchTerm)
                ? PerformanceReview.PastReviews
                : PerformanceReview.PastReviews
                    .Where(r => r.EmployeeName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            // Pass the trainings list to the view using ViewData
            ViewData["Trainings"] = Trainings;

            return View(model);
        }

        // Save the review and add it to the past reviews list
        [HttpPost]
        public IActionResult SaveReview(PerformanceReview model)
        {
            if (ModelState.IsValid)
            {
                // Add the current review to the list of past reviews
                PerformanceReview.PastReviews.Add(new PerformanceReview
                {
                    EmployeeName = model.EmployeeName,
                    ReviewDate = model.ReviewDate,
                    PerformanceRating = model.PerformanceRating,
                    Feedback = model.Feedback,
                });

                // Clear the form fields after saving
                ModelState.Clear();
            }

            // Return to the view with the updated model containing past reviews
            return RedirectToAction("CreateReview");
        }

        // POST: Save Training
        [HttpPost]
        public IActionResult SaveTraining(string EmployeeName, string CourseName, string CourseLink, DateTime CompletionDate)
        {
            // Create a new training record
            var newTraining = new Training
            {
                EmployeeName = EmployeeName,
                CourseName = CourseName,
                CourseLink = CourseLink,
                CompletionDate = CompletionDate,
                Status = true // Set status logic as needed (e.g., completed or incomplete)
            };

            // Add the new training record to the static list
            Trainings.Add(newTraining);

            // Redirect to the same CreateReview view to show updated list
            return RedirectToAction("CreateReview");
        }
    }
}
