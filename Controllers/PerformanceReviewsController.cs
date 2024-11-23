﻿using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using XBCAD7319_SparkLine_HR_WebApp.Models;
using XBCAD7319_SparkLine_HR_WebApp.Token;

namespace XBCAD7319_SparkLine_HR_WebApp.Controllers
{
    public class PerformanceReviewsController : Controller
    {
        // Static list to hold past training records
        private static List<Training> Trainings = new List<Training>();

        private readonly OnboardingService _onboardingService;
        private readonly FirebaseService _firebaseService;
        //private readonly OnboardingManager _onboardingManager;


        public PerformanceReviewsController()
        {
            _onboardingService = new OnboardingService();
            _firebaseService = new FirebaseService();
            //_onboardingManager = onboardingManager;
        }

        [TokenAuthorizationFilter]
        // Get the employee name and number to display in the dropdown list
        public async Task<IActionResult> CreateReview()
        {
            // Initialize Firebase client
            var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");

            // Fetch employee data
            var employees = await firebase
                .Child("SparkLineHR")
                .Child("employees_sparkline")
                .OnceAsync<dynamic>();

            // Map data for dropdown
            ViewData["EmployeeList"] = employees.Select(e => new SelectListItem
            {
                Value = e.Object["empID"], // empID as the value
                Text = e.Object["employee"]["Name"] // Name for display
            }).ToList();

            return View();
        }

        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<JsonResult> SubmitReviewAJAX(PerformanceReview review)
        {
            if (ModelState.IsValid)
            {
                var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");

                string childPerf = review.EmployeeNumber + "," + review.ReviewDate.ToString("yyyy-MM-dd");

                // Save the review under "performanceReviews" with a custom path
                await firebase
                    .Child("SparkLineHR")
                    .Child("performanceReviews")
                    .Child(childPerf)  // Use the custom key based on EmployeeNumber and ReviewDate
                    .PutAsync(new
                    {
                        EmployeeNumber = review.EmployeeNumber,
                        ReviewDate = review.ReviewDate,
                        PerformanceRating = review.PerformanceRating,
                        Feedback = review.Feedback
                    });

                TempData["SuccessMessage"] = "Review saved successfully!";
                return Json(new { success = true });
            }

            // Return failure response if model state is not valid
            return Json(new { success = false, message = "Invalid data. Please check the fields." });
        }

        [TokenAuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> GetPastReviews()
        {
            try
            {
                var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");
                var reviews = await firebase
                    .Child("SparkLineHR")
                    .Child("performanceReviews")
                    .OnceAsync<PerformanceReview>();

                // Use lowercase keys to match JavaScript expectations
                var reviewList = reviews.Select(r => new
                {
                    employeeNumber = r.Object.EmployeeNumber, // Lowercase keys
                    reviewDate = r.Object.ReviewDate.ToString("yyyy-MM-dd"),
                    performanceRating = r.Object.PerformanceRating,
                    feedback = r.Object.Feedback
                }).ToList();

                // Log to verify structure
                Console.WriteLine(JsonConvert.SerializeObject(reviewList));

                return Json(new { success = true, data = reviewList });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [TokenAuthorizationFilter]
        [HttpPost]
        public async Task<JsonResult> SubmitTrainingAJAX(Training training)
        {
            if (ModelState.IsValid)
            {
                var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");

                string childPerf = training.EmployeeNumber + "," + training.CompletionDate.ToString("yyyy-MM-dd");

                // Save the review under "performanceReviews" with a custom path
                await firebase
                    .Child("SparkLineHR")
                    .Child("trainings")
                    .Child(childPerf)  // Use the custom key based on EmployeeNumber and ReviewDate
                    .PutAsync(new
                    {
                        EmployeeNumber = training.EmployeeNumber,
                        CourseName = training.CourseName,
                        CourseLink = training.CourseLink,
                        CompletionDate = training.CompletionDate.ToString("yyyy-MM-dd")
                    });

                TempData["SuccessMessage"] = "Training saved successfully!";
                return Json(new { success = true });
            }

            // Return failure response if model state is not valid
            return Json(new { success = false, message = "Invalid data. Please check the fields." });
        }

        [TokenAuthorizationFilter]
        [HttpGet]
        public async Task<IActionResult> GetPastTrainings()
        {
            try
            {
                var firebase = new FirebaseClient("https://hrappstorage-default-rtdb.firebaseio.com/");
                var trainings = await firebase
                    .Child("SparkLineHR")
                    .Child("trainings")
                    .OnceAsync<Training>();

                // Use lowercase keys to match JavaScript expectations
                var trainingList = trainings.Select(r => new
                {
                    employeeNumber = r.Object.EmployeeNumber, // Lowercase keys
                    courseName = r.Object.CourseName,
                    courseLink = r.Object.CourseLink,
                    completionDate = r.Object.CompletionDate.ToString("yyyy-MM-dd")
                }).ToList();

                // Log to verify structure
                Console.WriteLine(JsonConvert.SerializeObject(trainingList));

                return Json(new { success = true, data = trainingList });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
