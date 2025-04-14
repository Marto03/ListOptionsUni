using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using HotelApp.Core.Repositories;

namespace HotelApp.BusinessLayer.Services
{
    internal class ReservationService : IReservationService
    {
        private readonly IGenericRepository<ReservationModel> _reservationRepository;
        private readonly IGenericRepository<PaymentModel> _paymentRepository;
        private readonly IGenericRepository<HotelFacilityModel> _hotelFacilityRepository;

        public ReservationService(IGenericRepository<ReservationModel> reservationRepository,
            IGenericRepository<PaymentModel> paymentRepository,
            IGenericRepository<HotelFacilityModel> hotelFacilityRepository)
        {
            _reservationRepository = reservationRepository;
            _paymentRepository = paymentRepository;
            _hotelFacilityRepository = hotelFacilityRepository;
        }

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

            // Използваме репозитория за добавяне на плащането
            await _paymentRepository.AddAsync(payment);

            // Създаваме новата резервация
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

            // Използваме репозитория за добавяне на резервацията
            await _reservationRepository.AddAsync(dbReservation);
        }

        public async Task<decimal> CalculateTotalPriceAsync(int? hotelId, List<int> usedFacilities, RoomTypeEnum roomType, int nightsNumber)
        {
            decimal totalPrice = 0;

            // Изчисляваме цената за удобствата
            foreach (int facilityId in usedFacilities)
            {
                var hotelFacilities = await _hotelFacilityRepository.GetAllAsync();
                var hotelFacility = hotelFacilities.FirstOrDefault(hf => hf.HotelId == hotelId && hf.FacilityId == facilityId);
                var currentDate = DateTime.Now;

                if (hotelFacility != null)
                {
                    decimal facilityPrice = hotelFacility.Price;

                    if (hotelFacility.DiscountPercentage.HasValue)
                    {
                        bool hasDateRange = hotelFacility.FromDate.HasValue && hotelFacility.ToDate.HasValue;

                        bool isInDateRange = hasDateRange && currentDate >= hotelFacility.FromDate.Value && currentDate <= hotelFacility.ToDate.Value;
                        if(!hasDateRange || isInDateRange)
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
            var reservations = await _reservationRepository.GetWithIncludesAsync(
                r => r.Hotel,
                r => r.User,
                r => r.Facilities,
                r => r.Payment
            );

            return reservations.Where(r => r.HotelId == hotelId).ToList();
        }

    }
}
