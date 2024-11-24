namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model for the Employee which has the employee id
    // the employee details object
    // the job details object
    // the document links
    // the payroll object
    // the leave balance object
    public class EmployeeDetailsViewModelAllFour
    {
        public string empID { get; set; }
        public Employee employee { get; set; }
        public JobDetails jobdetails { get; set; }
        public DocumentLinks documentlinks { get; set; }
        public Payroll payroll { get; set; }
        public LeaveBalance leavebalance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="empID">Employee id</param>
        /// <param name="employee">employee details object</param>
        /// <param name="jobdetails">job details object</param>
        /// <param name="documentlinks">the document links</param>
        /// <param name="payroll">the payroll object</param>
        /// <param name="leave">leave balance object</param>
        public EmployeeDetailsViewModelAllFour(string empID, Employee employee, JobDetails jobdetails, DocumentLinks documentlinks, Payroll payroll, LeaveBalance leave)
        {
            this.empID = empID;
            this.employee = employee;
            this.jobdetails = jobdetails;
            this.documentlinks = documentlinks;
            this.payroll = payroll;
            this.leavebalance = leave;
        }
    }
}
