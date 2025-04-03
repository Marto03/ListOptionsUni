using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IReservationRepository
    {
        void AddReservation(ReservationModel reservation);
        List<ReservationModel> GetAllReservations();
    }
}
