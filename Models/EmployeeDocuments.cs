using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    /// <summary>
    /// Model for the Employee Documnets 
    /// </summary>
    public class EmployeeDocuments
    {
        [Display(Name = "ID or Passport")]
        public IFormFile IDOrPassport { get; set; }

        [Display(Name = "CV")]
        public IFormFile CV { get; set; }

        [Display(Name = "Proof of Address")]
        public IFormFile ProofOfAddress { get; set; }

        [Display(Name = "Proof of Tax Registration")]
        public IFormFile ProofOfTaxRegistration { get; set; }

        [Display(Name = "Completed IRP5 Information")]
        public IFormFile CompletedIRP5 { get; set; }

        [Display(Name = "Proof of Bank Account")]
        public IFormFile ProofOfBank { get; set; }
    }
}
