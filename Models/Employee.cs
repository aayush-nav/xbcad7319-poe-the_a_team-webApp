using Microsoft.AspNetCore.Mvc.Rendering;

namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class Employee
    {
        // public string EmployeeId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmergencyContactRelationship { get; set; }

        public Employee(string name, DateTime dateOfBirth, string contact, string email, string address, string emergencyContactName, string emergencyContactNumber, string emergencyContactRelationship)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Contact = contact;
            Email = email;
            Address = address;
            EmergencyContactName = emergencyContactName;
            EmergencyContactNumber = emergencyContactNumber;
            EmergencyContactRelationship = emergencyContactRelationship;
        }

        public Employee()
        {
        }
    }
    //public class Employee
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string JobTitle { get; set; }
    //    public string Department { get; set; }
    //    public string Contact { get; set; }
    //    public string Email { get; set; }
    //    public string Address { get; set; }
    //    public DateTime DateOfBirth { get; set; }

    //    // Emergency contact fields
    //    public string EmergencyContactName { get; set; }
    //    public string EmergencyContactNumber { get; set; }
    //    public string EmergencyContactRelationship { get; set; } 

    //    // Dropdown options for Emergency Contact Relationship
    //    public List<SelectListItem> RelationshipOptions { get; set; } = new List<SelectListItem>
    //    {
    //        new SelectListItem { Value = "Parent", Text = "Parent" },
    //        new SelectListItem { Value = "Sibling", Text = "Sibling" },
    //        new SelectListItem { Value = "Spouse", Text = "Spouse" },
    //        new SelectListItem { Value = "Friend", Text = "Friend" },
    //        new SelectListItem { Value = "Grandparent", Text = "Grandparent" },
    //        new SelectListItem { Value = "Cousin", Text = "Cousin"},
    //        new SelectListItem { Value = "Other", Text = "Other"}
    //    };
    //}
}
