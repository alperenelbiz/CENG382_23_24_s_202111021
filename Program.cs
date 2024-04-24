using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

class Program
{
    static void Main(string[] args)
    {
        const string roomDataFilePath = "Data.json";
        const string logDataFilePath = "LogData.json";

        var factory = new Factory(roomDataFilePath, logDataFilePath);

        var reservationService = factory.CreateReservationService();
        var roomHandler = factory.CreateRoomHandler();
        var reservationHandler = factory.CreateReservationHandler();

        bool continueApp = true;
        while (continueApp)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add Reservation");
            Console.WriteLine("2. Delete Reservation");
            Console.WriteLine("3. Display This Week's Schedule");
            Console.WriteLine("4. Show Available Rooms");
            Console.WriteLine("5. Exit");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Wrong input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddReservation(reservationService, roomHandler);
                    break;
                case 2:
                    DeleteReservation(reservationService);
                    break;
                case 3:
                    reservationService.DisplayWeeklySchedule();
                    break;
                case 4:
                    var rooms = roomHandler.GetRooms();
                    try
                    {
                        reservationHandler.ShowRoomCapacities(rooms);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error: Invalid JSON format in 'data.json'. {ex.Message}");
                    }
                    break;
                case 5:
                    continueApp = false;
                    break;
                default:
                    Console.WriteLine("Wrong option. Please choose a valid option.");
                    break;
            }
        }
    }

    static void AddReservation(ReservationService reservationService, RoomHandler roomHandler)
    {
        string roomId = GetValidRoomId();

        Console.WriteLine("\nSelect Reservation Date and Time Slot:");
        var availableSlots = roomHandler.GetRoomTimeSlots(roomId);
        for (int i = 0; i < availableSlots.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {availableSlots[i].ToString("yyyy-MM-dd HH:mm")}");
        }

        int slotIndex;
        if (!int.TryParse(Console.ReadLine(), out slotIndex) || slotIndex < 1 || slotIndex > availableSlots.Count)
        {
            Console.WriteLine("Invalid choice. Please select a valid slot index.");
            return;
        }

        var chosenDateTime = availableSlots[slotIndex - 1];

        Console.WriteLine("\nEnter Name of Person Making Reservation:");
        string name = Console.ReadLine();

        var room = new Room { RoomId = roomId };
        var reservation = new Reservation(room, chosenDateTime, name);
        reservationService.AddReservation(reservation, name, chosenDateTime);
    }

    static void DeleteReservation(ReservationService reservationService)
    {
        string roomId = GetValidRoomId();

        Console.WriteLine("\nSelect Reservation Date and Time Slot to Delete:");
        var reservationsForRoom = reservationService.GetReservationsForRoom(roomId);
        for (int i = 0; i < reservationsForRoom.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {reservationsForRoom[i].DateTime.ToString("yyyy-MM-dd HH:mm")} - Reserved by: {reservationsForRoom[i].ReservedBy}");
        }

        int reservationIndex;
        if (!int.TryParse(Console.ReadLine(), out reservationIndex) || reservationIndex < 1 || reservationIndex > reservationsForRoom.Count)
        {
            Console.WriteLine("Invalid choice. Please select a valid reservation index.");
            return;
        }

        var chosenReservation = reservationsForRoom[reservationIndex - 1];

        Console.WriteLine("\nEnter Your Name for Verification:");
        string name = Console.ReadLine();

        if (chosenReservation.ReservedBy != name)
        {
            Console.WriteLine("Name does not match the reservation. Delete operation aborted.");
            return;
        }

        reservationService.DeleteReservation(chosenReservation, name);
    }


    static string GetValidRoomId()
    {
        Console.WriteLine("Enter Room ID:");
        string roomId;
        do
        {
            roomId = Console.ReadLine();
            if (string.IsNullOrEmpty(roomId))
            {
                Console.WriteLine("Room ID cannot be empty. Please enter a valid ID:");
            }
        } while (string.IsNullOrEmpty(roomId));
        return roomId;
    }
}
