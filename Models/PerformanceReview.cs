using System;

namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class PerformanceReview
    {
        public string EmployeeName { get; set; }
        public DateTime ReviewDate { get; set; }
        public string PerformanceRating { get; set; }
        public string Feedback { get; set; }

        // List to hold past reviews
        public static List<PerformanceReview> PastReviews { get; set; } = new List<PerformanceReview>();
    }
}
