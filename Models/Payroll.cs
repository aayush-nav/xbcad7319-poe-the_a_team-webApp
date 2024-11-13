﻿namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class Payroll
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Bank { get; set; }
        public string BankAccountNumber { get; set; }
        public string AccountType { get; set; }
        public string BranchCode { get; set; }
        public string TaxNumber { get; set; }
        public string AccountName { get; set;}
        public string PAYETax { get; set; }
        public string UIFContribution { get; set;}
        public string RetirementContribution { get; set; }
    }
}
