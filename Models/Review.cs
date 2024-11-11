namespace XBCAD7319_SparkLine_HR_WebApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Rating { get; set; }
        public string Reviewer { get; set; }
        public string Feedback { get; set; }
    }
}
