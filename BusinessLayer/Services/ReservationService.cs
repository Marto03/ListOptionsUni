using DataLayer.Models;
using DataLayer.Repositories;

namespace BusinessLayer.Services
{
    public class ReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        //public void AddReservation(ReservationModel reservation)
        //{
        //    if (reservation.Payment == null)
        //        throw new InvalidOperationException("Резервацията трябва да има плащане!");

        //    // Проверка за -10% намаление, ако удобството е басейн или друго несистемно дефинирано удобство
        //    if (reservation.UsedFacilities.Any(f => f.Type == FacilityTypeEnum.Pool))
        //    {
        //        reservation.Price *= 0.9m; // Намаляваме цената с 10%
        //    }

        //    _reservationRepository.AddReservation(reservation);
        //}

        public List<ReservationModel> GetAllReservations()
        {
            return _reservationRepository.GetAllReservations();
        }
    }
}
