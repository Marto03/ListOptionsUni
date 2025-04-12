using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using HotelApp.Core.Repositories;

namespace HotelApp.BusinessLayer.Services
{
    internal class HotelService : IHotelService
    {
        private readonly IGenericRepository<HotelModel> _hotelRepository;

        public HotelService(IGenericRepository<HotelModel> hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public void AddHotel(HotelModel hotel)
        {
            if (_hotelRepository.GetAll().Any(h => h.Name == hotel.Name))
                throw new InvalidOperationException("Хотел с това име вече съществува!");

            _hotelRepository.Add(hotel);
        }

        public List<HotelModel> GetAllHotels()
        {
            return _hotelRepository.GetAll();
        }
    }
}
