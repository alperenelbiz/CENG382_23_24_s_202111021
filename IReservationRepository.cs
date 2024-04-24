public interface IReservationRepository
{
    void AddReservation(Reservation reservation);
    bool DeleteReservation(Reservation reservation);
    List<Reservation> GetAllReservations();
}