namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class EmployeeDetailsViewModelAllFour
    {
        public string empID { get; set; }
        public Employee employee { get; set; }
        public JobDetails jobdetails { get; set; }

        public DocumentLinks documentlinks { get; set; }

        public Payroll payroll { get; set; }

        public EmployeeDetailsViewModelAllFour(string empID, Employee employee, JobDetails jobdetails, DocumentLinks documentlinks, Payroll payroll)
        {
            this.empID = empID;
            this.employee = employee;
            this.jobdetails = jobdetails;
            this.documentlinks = documentlinks;
            this.payroll = payroll;
        }
    }
}
