namespace lab11.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
