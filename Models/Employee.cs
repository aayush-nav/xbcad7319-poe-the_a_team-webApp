using Microsoft.AspNetCore.Mvc.Rendering;

namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model for the Employee Details
    public class Employee
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmergencyContactRelationship { get; set; }     
    }  
}
