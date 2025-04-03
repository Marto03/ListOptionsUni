using DataLayer.Models;
using DataLayer.Repositories;

namespace BusinessLayer.Services
{
    public class HotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public void AddHotel(HotelModel hotel)
        {
            if (_hotelRepository.GetAllHotels().Any(h => h.Name == hotel.Name))
                throw new InvalidOperationException("Хотел с това име вече съществува!");

            _hotelRepository.AddHotel(hotel);
        }

        public List<HotelModel> GetAllHotels()
        {
            return _hotelRepository.GetAllHotels();
        }
    }
}
