using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class LogHandler
{
    private readonly ILogger _logger;
    private readonly IReservationRepository _reservationRepository;
    private readonly string _reservationDataFilePath;


    public LogHandler(ILogger logger, IReservationRepository reservationRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _reservationRepository = reservationRepository;

        _reservationDataFilePath = "ReservationData.json";

    }

    public void AddLog(LogRecord logRecord)
    {
        if (logRecord == null)
        {
            throw new ArgumentNullException(nameof(logRecord));
        }

        _logger.Log(logRecord);
    }
    public void LogReservationsToFile()
    {
        try
        {
            // Get all reservations
            var reservations = _reservationRepository.GetAllReservations();

            // Convert reservations to JSON
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(reservations, options);

            // Write JSON to file
            File.WriteAllText(_reservationDataFilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error logging reservations: {ex.Message}");
        }
    }
}
