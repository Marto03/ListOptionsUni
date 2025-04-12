namespace HotelApp.Core.DTOs
{
    public class RoomTypeDTO
    {
        public RoomTypeEnum Name { get; set; }

        public static readonly List<RoomTypeDTO> DefaultRoomTypes = new List<RoomTypeDTO>
        {
            new RoomTypeDTO { Name = RoomTypeEnum.Single},
            new RoomTypeDTO { Name = RoomTypeEnum.Double},
            new RoomTypeDTO { Name = RoomTypeEnum.Luxury}
        };
    }
}
