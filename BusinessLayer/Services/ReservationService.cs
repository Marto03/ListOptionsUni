using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class ReservationService : BaseService
    {
        public ReservationService(HotelDbContextModel context) : base(context) { }

        public async Task AddReservationAsync(ReservationModel reservation)
        {
            var totalPrice = await CalculateTotalPriceAsync(reservation.HotelId, reservation.UsedFacilities, reservation.RoomType, Math.Max(1, (int)(reservation.CheckOutDate - reservation.CheckInDate).TotalDays));

            // Създаваме PaymentModel
            var payment = new PaymentModel
            {
                Amount = totalPrice,
                HotelId = reservation.HotelId,
                PaymentMethodId = reservation.PaymentId
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            var hotelExists = await _context.Hotels.AnyAsync(h => h.Id == reservation.HotelId);
            var userExists = await _context.Users.AnyAsync(u => u.Id == reservation.UserId);
            var paymentExists = await _context.Payments.AnyAsync(p => p.Id == reservation.PaymentId);
            var facilitiesExist = await _context.Facilities
                .Where(f => reservation.UsedFacilities.Contains(f.Id))
                .CountAsync() == reservation.UsedFacilities.Count;

            var dbReservation = new ReservationModel
            {
                HotelId = reservation.HotelId,
                UserId = reservation.UserId,
                RoomType = reservation.RoomType,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                Price = totalPrice,
                PaymentId = payment.Id,
                UsedFacilities = reservation.UsedFacilities
                
            };

            _context.Reservations.Add(dbReservation);
            await _context.SaveChangesAsync();

        }

        public async Task<decimal> CalculateTotalPriceAsync(int? hotelId, List<int> usedFacilities, RoomTypeEnum roomType, int nightsNumber)
        {
            decimal totalPrice = 0;

            // Изчисляваме цената за удобствата
            foreach (int facilityId in usedFacilities)
            {
                var hotelFacility = await _context.HotelFacilities
                    .FirstOrDefaultAsync(hf => hf.HotelId == hotelId && hf.FacilityId == facilityId);

                if (hotelFacility != null)
                {
                    decimal facilityPrice = hotelFacility.Price;

                    if (hotelFacility.DiscountPercentage.HasValue)
                    {
                        facilityPrice -= facilityPrice * (decimal)(hotelFacility.DiscountPercentage.Value / 100.0);
                    }

                    totalPrice += facilityPrice;
                }
            }

            // Примерна базова цена за типа стая
            decimal baseRoomPrice = roomType switch
            {
                RoomTypeEnum.Single => 50,
                RoomTypeEnum.Double => 80,
                RoomTypeEnum.Luxury => 120,
                _ => 0
            };

            totalPrice += baseRoomPrice;

            return totalPrice * nightsNumber;
        }
        public async Task<List<ReservationModel>> GetReservationsByHotelAsync(int hotelId)
        {
            // Използваме Include за да заредим свързаните обекти по ID
            var reservations = await _context.Reservations
                                              .Where(r => r.HotelId == hotelId)
                                              .Include(r => r.Hotel)               // Зареждаме информацията за хотела
                                              .Include(r => r.User)                // Зареждаме информацията за потребителя
                                              .Include(r => r.Facilities)          // Зареждаме свързаните удобства
                                              .Include(r => r.Payment)       // Зареждаме информацията за метода на плащане
                                              .ToListAsync();
            return reservations;
        }

    }

}
