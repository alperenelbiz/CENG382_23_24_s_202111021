public class LogRecord
{
    public DateTime Timestamp { get; set; }
    public string? ReserverName { get; set; }
    public string? RoomName { get; set; }
    public string? Action { get; set; }
    public DateTime DateTime { get; set; }

    public static LogRecord CreateReservationLog_A(string reserverName, string roomId, DateTime dateTime)
    {
        return new LogRecord
        {
            Timestamp = DateTime.Now,
            Action = "Reservation added",
            ReserverName = reserverName,
            RoomName = roomId,
            DateTime = dateTime
        };
    }

    public static LogRecord CreateReservationLog_D(string reserverName, string roomId, DateTime dateTime)
    {
        return new LogRecord
        {
            Timestamp = DateTime.Now,
            Action = "Reservation deleted",
            ReserverName = reserverName,
            RoomName = roomId,
            DateTime = dateTime
        };
    }

}