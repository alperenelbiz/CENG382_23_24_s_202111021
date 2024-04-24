using System.Text.Json;

public class ReservationHandler
{
    private readonly IReservationRepository _reservationRepository;
    private readonly RoomHandler _roomHandler;
    private readonly string _reservationDataFilePath;
    private readonly LogHandler _logHandler;
    private Dictionary<string, List<DateTime>> _roomTimeSlots;

    public ReservationHandler(IReservationRepository reservationRepository, RoomHandler roomHandler, LogHandler logHandler)
    {
        _reservationRepository = reservationRepository;
        _roomHandler = roomHandler;
        _logHandler = logHandler;
        // Generate time slots for each room
        var rooms = _roomHandler.GetRooms();
        _roomTimeSlots = roomHandler.GenerateTimeSlotsForRooms(rooms);
    }

    public bool AddReservation(Reservation reservation, string reserverName, DateTime chosenDateTime)
    {
        // Check if the chosen date and time slot is available for the selected room
        if (IsTimeSlotAvailable(reservation.Room.RoomId, chosenDateTime))
        {
            reservation.DateTime = chosenDateTime; // Set the reservation date and time
            _reservationRepository.AddReservation(reservation); // Add the reservation

            // Log reservation added successfully
            var logRecord = LogRecord.CreateReservationLog_A(reserverName, reservation.Room.RoomId, chosenDateTime);
            _logHandler.AddLog(logRecord);

            _logHandler.LogReservationsToFile();
            Console.WriteLine("\nReservation added successfully.\n");

            return true; // Return true to indicate successful addition
        }
        else
        {
            Console.WriteLine("Selected date and time slot is not available for the chosen room.");
            return false; // Return false to indicate failed addition
        }
    }

    public void DeleteReservation(Reservation reservation, string reserverName)
    {
        try
        {
            _reservationRepository.DeleteReservation(reservation);

            var logRecord = LogRecord.CreateReservationLog_D(reserverName, reservation.Room.RoomId, reservation.DateTime);
            _logHandler.AddLog(logRecord);

            _logHandler.LogReservationsToFile();
            Console.WriteLine("\nReservation deleted successfully.\n");


        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete reservation: {ex.Message}");
        }
    }
    private bool IsTimeSlotAvailable(string roomId, DateTime chosenDateTime)
    {
        if (_roomTimeSlots.ContainsKey(roomId))
        {
            // Check if the chosen date and time slot is within the predefined time slots for the room
            if (!_roomTimeSlots[roomId].Contains(chosenDateTime))
            {
                Console.WriteLine("Selected date and time slot is not within the available time slots for the chosen room.");
                return false;
            }

            // Check if the chosen time slot is already reserved
            var reservationsForRoom = _reservationRepository.GetAllReservations().Where(r => r.Room.RoomId == roomId);
            if (reservationsForRoom.Any(r => r.DateTime == chosenDateTime))
            {
                Console.WriteLine("Selected date and time slot is already reserved for the chosen room.");
                return false;
            }

            return true;
        }
        else
        {
            Console.WriteLine("Invalid room ID.");
            return false;
        }
    }
    public void DisplayScheduleForWeek()
    {

        var reservations = _reservationRepository.GetAllReservations();

        Console.WriteLine($"Total reservations: {reservations.Count}");
        Console.WriteLine("\n-------------------------------------------------------");
        Console.WriteLine("|   Date     |   Time    |     Room    |  Reserved By |");
        Console.WriteLine("-------------------------------------------------------");


        foreach (var reservation in reservations)
        {
            if (reservation != null) // Check for current week
            {
                Console.WriteLine($"| {reservation.DateTime.ToShortDateString(),-10} | {reservation.DateTime.ToShortTimeString(),-8} | {reservation.Room.RoomId,-12} | {reservation.ReservedBy,-12} |");
            }
        }

        Console.WriteLine("-------------------------------------------------------\n");
    }
    public void ShowRoomCapacities(List<Room> rooms)
    {
        Console.WriteLine("\n-------------------------------------------------------");
        Console.WriteLine("| Room ID | Room Name     | Remaining Capacity |");
        Console.WriteLine("-------------------------------------------------------");

        foreach (var room in rooms)
        {
            int existingReservations = _reservationRepository.GetAllReservations().Count(r => r.Room.RoomId == room.RoomId);
            int remainingCapacity = room.Capacity - existingReservations;

            Console.WriteLine($"| {room.RoomId,-7} | {room.RoomName,-13} | {remainingCapacity,-18} |");
        }

        Console.WriteLine("-------------------------------------------------------\n");
    }
    public List<Reservation> GetReservationsForRoom(string roomId)
    {
        // Retrieve all reservations for the specified room ID from the repository
        return _reservationRepository.GetAllReservations().Where(r => r.Room.RoomId == roomId).ToList();
    }

}