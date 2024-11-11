namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class CalendarViewModel
    {
        public List<WeekViewModel> Weeks { get; set; }
    }

    public class WeekViewModel
    {
        public List<DayViewModel> Days { get; set; }
    }

    public class DayViewModel
    {
        public DateTime Date { get; set; }
        public string Status { get; set; } 
    }
}
