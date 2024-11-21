using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class JobDetails
    {
        public string EmployeeId { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string Manager { get; set; }
        public string EmploymentType { get; set; }
        public DateTime HireDate { get; set; }
        public string JobDescription { get; set; }

        public JobDetails(string employeeId, string jobTitle, string department, string manager, string employmentType, DateTime hireDate, string jobDescription)
        {
            EmployeeId = employeeId;
            JobTitle = jobTitle;
            Department = department;
            Manager = manager;
            EmploymentType = employmentType;
            HireDate = hireDate;
            JobDescription = jobDescription;
        }

        public JobDetails()
        {
        }
    }
}
