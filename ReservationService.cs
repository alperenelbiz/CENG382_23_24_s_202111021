public class ReservationService : IReservationService
{
    private readonly ReservationHandler _reservationHandler;
    private readonly LogHandler _logHandler;
    public RoomHandler RoomHandler { get; }


    public ReservationService(ReservationHandler reservationHandler, LogHandler logHandler, RoomHandler roomHandler)
    {
        _reservationHandler = reservationHandler;
        _logHandler = logHandler;
        RoomHandler = roomHandler;

    }

        public bool AddReservation(Reservation reservation, string reserverName, DateTime chosenDateTime)
    {
        if (_reservationHandler.AddReservation(reservation, reserverName, chosenDateTime))
        {
            Console.WriteLine("\nReservation added successfully.\n");
            return true; // Return true to indicate successful addition
        }
        else
        {
            return false; // Return false if reservation addition fails
        }
    }

        public List<Reservation> GetReservationsForRoom(string roomId)
    {
        // Retrieve all reservations for the specified room ID
        return _reservationHandler.GetReservationsForRoom(roomId);
    }

    public void DeleteReservation(Reservation reservation, string reserverName)
    {
        try
        {
            _reservationHandler.DeleteReservation(reservation, reserverName); // Call the method even if it doesn't return bool
            Console.WriteLine("\nReservation deleted successfully.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while deleting reservation: {ex.Message}");
            // Optionally log the exception details using _logHandler
        }
    }


    public void DisplayWeeklySchedule()
    {
        _reservationHandler.DisplayScheduleForWeek();
    }
}