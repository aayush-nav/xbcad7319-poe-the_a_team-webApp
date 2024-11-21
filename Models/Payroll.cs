namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class Payroll
    {
        public string GrossPay { get; set; }
        //public string PAYE { get; set; }
        public string UIF { get; set; }
        public string Retirement { get; set; }
        public string PaymentDate { get; set; }
        public string TaxNumber { get; set; }
        public string AccountName { get; set; }
        public string Bank { get; set; }
        public string BankAccountNumber { get; set; }
        public string AccountType { get; set; }
        public string BranchCode { get; set; }

        public Payroll()
        {
        }
    }
}
