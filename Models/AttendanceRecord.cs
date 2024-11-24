namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    // Model for the Attendace Record
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public string? Status { get; set; }
    }
}
