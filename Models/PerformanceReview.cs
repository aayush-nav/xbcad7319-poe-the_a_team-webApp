using System;

namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model for the performance reviews
    public class PerformanceReview
    {
        public string EmployeeNumber { get; set; } // empID from the dropdown
        public DateTime ReviewDate { get; set; }
        public string PerformanceRating { get; set; }
        public string Feedback { get; set; }
    }
    
    // Model for the traing courses
    public class Training
    {
        public string EmployeeNumber { get; set; } // empID from the dropdown
        public string CourseName { get; set; }
        public string CourseLink { get; set; }
        public DateTime CompletionDate { get; set; }
    }
  
}
