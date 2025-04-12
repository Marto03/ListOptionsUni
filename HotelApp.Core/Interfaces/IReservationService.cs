using HotelApp.Core.Models;

namespace HotelApp.Core.Interfaces
{
    public interface IReservationService
    {
        Task AddReservationAsync(ReservationModel reservation);

        Task<decimal> CalculateTotalPriceAsync(int? hotelId, List<int> usedFacilities, RoomTypeEnum roomType, int nightsNumber);

        Task<List<ReservationModel>> GetReservationsByHotelAsync(int hotelId);

    }
}
