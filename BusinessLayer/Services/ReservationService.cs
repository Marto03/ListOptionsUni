using DataLayer.Models;

namespace BusinessLayer.Services
{
    public class ReservationService : BaseService
    {
        public ReservationService(HotelDbContextModel context) : base(context) { }

        public async Task AddReservationAsync(
        int hotelId,
        int userId,
        RoomTypeEnum roomType,
        DateTime checkIn,
        DateTime checkOut,
        List<HotelFacilityModel> selectedFacilities,
        int paymentMethodId)
        {
            decimal basePrice = GetBaseRoomPrice(roomType);

            // Изчисли цена на удобства с отстъпка
            decimal facilityTotalPrice = 0;
            foreach (var facility in selectedFacilities)
            {
                decimal discountPercentage = facility.DiscountPercentage.HasValue
                    ? (decimal)facility.DiscountPercentage.Value
                    : 0;

                decimal discountMultiplier = (100 - discountPercentage) / 100;
                facilityTotalPrice += facility.Price * discountMultiplier;
            }

            int numberOfNights = Math.Max(1, (int)(checkOut - checkIn).TotalDays);
            decimal totalPrice = (basePrice + facilityTotalPrice) * numberOfNights;

            // Създай плащане
            var payment = new PaymentModel
            {
                Amount = totalPrice,
                PaymentDate = DateTime.Now,
                PaymentMethodId = paymentMethodId
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            // !!! Забележка: ReservationModel очаква List<FacilityModel>, а не HotelFacilityModel.
            // Трябва да конвертираме избраните HotelFacilityModel към FacilityModel.

            var facilityModels = selectedFacilities
                .Select(f => _context.Facilities.FirstOrDefault(x => x.Id == f.FacilityId))
                .Where(x => x != null)
                .ToList();

            var reservation = new ReservationModel
            {
                HotelId = hotelId,
                UserId = userId,
                RoomType = roomType,
                CheckInDate = checkIn,
                CheckOutDate = checkOut,
                Price = totalPrice,
                UsedFacilities = facilityModels,
                PaymentId = payment.Id
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        private decimal GetBaseRoomPrice(RoomTypeEnum roomType)
        {
            return roomType switch
            {
                RoomTypeEnum.Single => 50,
                RoomTypeEnum.Double => 80,
                RoomTypeEnum.Luxury => 150,
                _ => 0
            };
        }
    }

}
