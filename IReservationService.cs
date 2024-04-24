public interface IReservationService
{
    bool AddReservation(Reservation reservation, string reserverName, DateTime chosenDateTime);
    void DeleteReservation(Reservation reservation, string reserverName);
    List<Reservation> GetReservationsForRoom(string roomId);

    void DisplayWeeklySchedule();
}