using Common;
using Common.DTOs;

namespace DataLayer.Services
{
    public class RoomTypeService
    {
        public List<RoomTypeDTO> GetRoomTypes()
        {
            
            return RoomTypeDTO.DefaultRoomTypes;  // Връща статичните стойности за типовете стаи
        }
    }
}
