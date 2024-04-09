using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

public class RoomData
{
    [JsonPropertyName("Room")]
    public Room[] Rooms { get; set; }
    public class Room
    {
        [JsonPropertyName("roomId")]
        public string roomID { get; set; }

        [JsonPropertyName("roomName")]
        public string roomName { get; set; }

        [JsonPropertyName("capacity")]
        public int capacity { get; set; }
    }

    public class Reservation
    {
        public DateTime DateTime { get; set; }
        public string ReserverName { get; set; }
        public Room Room { get; set; }
    }

    public class ReservationHandler
    {
        public List<Reservation> reservations = new List<Reservation>();

        public bool AddReservation(Reservation reservation)
        {
            foreach (var existingReservation in reservations)
            {
                if (existingReservation.DateTime == reservation.DateTime && existingReservation.Room == reservation.Room)
                {
                    Console.WriteLine("There's already a reservation for this time and room.");
                    return false;
                }
            }

            reservations.Add(reservation);
            Console.WriteLine("Reservation added successfully.");
            return true;
        }

        public void DeleteReservation(Reservation reservation)
        {
            reservations.Remove(reservation);
            Console.WriteLine("Reservation deleted successfully.");
        }

        public void DisplayWeeklySchedule()
        {
            foreach (var reservation in reservations)
            {
                Console.WriteLine($"Date: {reservation.DateTime.ToShortDateString()}, Time: {reservation.DateTime.ToShortTimeString()}, Room: {reservation.Room.roomName}, Reserver: {reservation.ReserverName}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string jsonFilePath = "Data.json";

            string jsonString = File.ReadAllText(jsonFilePath);


            var options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString
            };

            var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, options);

            ReservationHandler reservationHandler = new ReservationHandler();

            if (roomData?.Rooms != null)
            {
                foreach (var room in roomData.Rooms)
                {
                    Console.WriteLine($"Room ID: {room.roomID}, Name: {room.roomName}, Capacity: {room.capacity}");
                }
            }

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Reservation");
                Console.WriteLine("2. Delete Reservation");
                Console.WriteLine("3. Display Weekly Schedule");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nAdd Reservation:");

                        Console.Write("Enter reserver name: ");
                        string reserverName = Console.ReadLine();

                        Console.Write("Enter reservation date (MM/DD/YYYY): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Available Rooms:");
                        for (int i = 0; i < roomData.Rooms.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}. {roomData.Rooms[i].roomName}");
                        }

                        Console.Write("We can take reservations between 9:00 to 20:00! ");
                        Console.Write("Enter time period (e.g., 9 for 9:00 - 10:00, 10 for 10:00 - 11:00): ");
                        int timePeriod = int.Parse(Console.ReadLine());
                        if (timePeriod < 9 || timePeriod > 20)
                        {
                            Console.WriteLine("Invalid time period.");
                            break;
                        }

                        DateTime time = new DateTime(date.Year, date.Month, date.Day, timePeriod, 0, 0);

                        Reservation newReservation = new Reservation
                        {
                            DateTime = time,
                            ReserverName = reserverName,
                            Room = roomData.Rooms[timePeriod]
                        };

                        reservationHandler.AddReservation(newReservation);
                        break;

                    case "2":
                        Console.WriteLine("\nDelete Reservation:");

                        if (reservationHandler.reservations.Count == 0)
                        {
                            Console.WriteLine("No reservations to delete.");
                            break;
                        }

                        Console.WriteLine("Current Reservations:");
                        for (int i = 0; i < reservationHandler.reservations.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. Date: {reservationHandler.reservations[i].DateTime.ToShortDateString()}, Time: {reservationHandler.reservations[i].DateTime.ToShortTimeString()}, Room: {reservationHandler.reservations[i].Room.roomName}, Reserver: {reservationHandler.reservations[i].ReserverName}");
                        }

                        Console.Write("Enter reservation number to delete: ");
                        int reservationIndex = int.Parse(Console.ReadLine()) - 1;
                        if (reservationIndex < 0 || reservationIndex >= reservationHandler.reservations.Count)
                        {
                            Console.WriteLine("Invalid reservation number.");
                            break;
                        }

                        reservationHandler.DeleteReservation(reservationHandler.reservations[reservationIndex]);
                        break;

                    case "3":
                        Console.WriteLine("\nWeekly Schedule:");
                        reservationHandler.DisplayWeeklySchedule();
                        break;

                    case "4":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
