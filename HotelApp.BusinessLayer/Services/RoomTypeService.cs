using HotelApp.Core.DTOs;
using HotelApp.Core.Interfaces;

namespace HotelApp.BusinessLayer.Services
{
    internal class RoomTypeService : IRoomTypeService
    {
        public List<RoomTypeDTO> GetRoomTypes()
        {
            
            return RoomTypeDTO.DefaultRoomTypes;  // Връща статичните стойности за типовете стаи
        }
    }
}
