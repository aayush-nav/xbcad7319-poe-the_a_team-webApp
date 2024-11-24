using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model for the Job details
    public class JobDetails
    {
        public string? EmployeeId { get; set; }
        public string? JobTitle { get; set; }
        public string? Department { get; set; }
        public string? Manager { get; set; }
        public string? EmploymentType { get; set; }
        public DateTime HireDate { get; set; }
        public string? JobDescription { get; set; }
    }
}
