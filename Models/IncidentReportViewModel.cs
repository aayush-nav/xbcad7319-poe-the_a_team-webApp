namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    /// <summary>
    /// Model for the incident report to be displyed in the table
    /// </summary>
    public class IncidentReportViewModel
    {
        public string EmployeeNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateSubmitted { get; set; }
    }

}
