using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class RoomData
{
    public List<Room> Room { get; set; }
}

public class RoomHandler
{
    private readonly string _filePath;
private readonly IReservationRepository _reservationRepository;

    public RoomHandler(string filePath)
    {
        _filePath = filePath;
    }

    public List<Room> GetRooms()
    {
        try
        {
            var jsonString = File.ReadAllText(_filePath);
            var options = new JsonSerializerOptions()
            {
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
            };
            var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);
            return roomData.Room;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            throw; // Rethrow the exception to indicate failure
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw; // Rethrow the exception to indicate failure
        }
    }

public List<DateTime> GetRoomTimeSlots(string roomId)
{
    // Implement logic to retrieve time slots for the given room
    // For this example, let's generate time slots from 9 am to 6 pm for each day

    List<DateTime> timeSlots = new List<DateTime>();

    DateTime currentDate = DateTime.Today.Date; // Start from today and set the time to 9 am

    // Generate time slots from 9 am to 6 pm
    for (int i = 9; i <= 18; i++)
    {
        // Add the current hour to the time slots list
        timeSlots.Add(currentDate.AddHours(i));
    }

    return timeSlots;
}
    public Dictionary<string, List<DateTime>> GenerateTimeSlotsForRooms(List<Room> rooms)
    {
        // Define your logic to generate time slots for each room here
        Dictionary<string, List<DateTime>> roomTimeSlots = new Dictionary<string, List<DateTime>>();

        foreach (var room in rooms)
        {
            List<DateTime> timeSlots = new List<DateTime>();
            DateTime currentDate = DateTime.Today.Date; // Start from today and set the time to 9 am

            // Start generating time slots from 9 am to 6 pm
            for (int i = 9; i <= 18; i++)
            {
                // Add the current hour to the time slots list
                timeSlots.Add(currentDate.AddHours(i));
            }

            // Associate the generated time slots with the room ID
            roomTimeSlots.Add(room.RoomId, timeSlots);
        }

        return roomTimeSlots;
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
}