namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class Payslip
    {
        public decimal GrossSalary { get; set; } // e.g., 93000
        public string Company { get; set; } // e.g., "SparkLine"
        public string EmpName { get; set; } // Employee name
        public string EmpNum { get; set; } // Employee ID
        public string EmpPos { get; set; } // Employee position
        public string IssueDate { get; set; } // e.g., "31-07-2024"
        public string PayslipPeriod { get; set; } // e.g., "01-07-2024 - 31-07-2024"
        public int PensionPercent { get; set; } // e.g., 3
        public string TaxNum { get; set; } // Tax number
        public int UIFPercent { get; set; } // e.g., 1
    }


    public class PayslipRequest
    {
        public string EmpId { get; set; } // Employee ID
        public string MonthYear { get; set; } // Format: "July 2024"
    }

}
