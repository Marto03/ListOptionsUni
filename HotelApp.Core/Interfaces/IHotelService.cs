using HotelApp.Core.Models;

namespace HotelApp.Core.Interfaces
{
    public interface IHotelService
    {
        void AddHotel(HotelModel hotel);


        List<HotelModel> GetAllHotels();
    }
}
