namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model for the onboadring manager which stored the values of the 4 objects when saving the user details 
    // used in onboarding and updating the employee
    public class OnboardingManager
    {
        public static Employee _employee;
        public static JobDetails _jobDetails;
        public static DocumentLinks _documentLinks;
        public static Payroll _payroll;
        public static LeaveBalance _leaveBalance;
    }
}
