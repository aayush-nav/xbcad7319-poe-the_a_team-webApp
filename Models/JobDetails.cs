﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class JobDetails
    {
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string Manager { get; set; }
        public string EmploymentType { get; set; } // Full-time, Part-time, etc.
        public DateTime HireDate { get; set; }
        public string Status { get; set; } // Active, On Leave, etc.
        public string JobDescription { get; set; } // New field  

        // Dropdown options
        public List<SelectListItem> RoleOptions { get; set; }
        public List<SelectListItem> DepartmentOptions { get; set; }
        public List<SelectListItem> EmployeeTypeOptions { get; set; }
        public List<SelectListItem> StatusOptions { get; set; }
    }
}
