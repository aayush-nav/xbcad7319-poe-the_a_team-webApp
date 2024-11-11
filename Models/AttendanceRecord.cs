namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public string Status { get; set; }
    }
}
