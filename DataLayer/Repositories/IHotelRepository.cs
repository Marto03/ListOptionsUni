using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IHotelRepository
    {
        void AddHotel(HotelModel hotel);
        List<HotelModel> GetAllHotels();
    }
}
