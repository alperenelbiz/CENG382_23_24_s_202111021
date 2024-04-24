public class Reservation
{
    public Room Room { get; set; }
    public DateTime DateTime { get; set; }
    public string ReservedBy { get; set; }

    public Reservation(Room room, DateTime dateTime, string reservedBy)
    {
        Room = room;
        DateTime = dateTime;
        ReservedBy = reservedBy;
    }

    public override string ToString()
    {
        return $"Room: {Room.RoomId} - {Room.RoomName}, Date: {DateTime.ToShortDateString()}, Time: {DateTime.ToShortTimeString()}, Reserved By: {ReservedBy}";
    }
}