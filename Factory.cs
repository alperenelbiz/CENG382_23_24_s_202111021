using System;
public class Factory
{
    private readonly string _roomDataFilePath;
    private readonly string _logDataFilePath;

    public Factory(string roomDataFilePath, string logDataFilePath)
    {
        _roomDataFilePath = roomDataFilePath;
        _logDataFilePath = logDataFilePath;
    }

    public RoomHandler CreateRoomHandler()
    {
        return new RoomHandler(_roomDataFilePath);
    }

    public ReservationRepository CreateReservationRepository()
    {
        return new ReservationRepository();
    }

    public ILogger CreateLogger()
    {
        return new FileLogger(_logDataFilePath);
    }

    public LogHandler CreateLogHandler()
    {
        return new LogHandler(CreateLogger(), CreateReservationRepository());
    }

    public ReservationHandler CreateReservationHandler()
    {
        return new ReservationHandler(CreateReservationRepository(), CreateRoomHandler(), CreateLogHandler());
    }

    public ReservationService CreateReservationService()
    {
        return new ReservationService(CreateReservationHandler(), CreateLogHandler(), CreateRoomHandler());
    }
}