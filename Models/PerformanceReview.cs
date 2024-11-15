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

    public class Training
    {
        public string EmployeeName { get; set; }
        public string CourseName { get; set; }
        public string CourseLink { get; set; }
        public DateTime CompletionDate { get; set; }
        public bool Status { get; set; } // "Complete" or "Incomplete"
    }
}
