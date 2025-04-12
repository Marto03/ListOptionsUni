using HotelApp.Core.DTOs;
using HotelApp.Core.Models;

namespace HotelApp.Core.Interfaces
{
    public interface IHotelFacilityService
    {
        List<HotelFacilityModel> GetFacilitiesByHotelId(int? hotelId);
        string? GetHotelNameByUserHotelId(int? hotelId);

        void ReplaceFacilitiesForHotel(int hotelId, List<HotelFacilityModel> newFacilities);

        List<HotelFacilityDTO> GetFacilitiesForHotel(int? hotelId);


        void SaveFacilitiesForHotel(int hotelId, List<HotelFacilityDTO> facilityDtos);

        void RemoveAllFacilitiesForHotel(int hotelId);
        List<HotelModel> GetHotelsByFacilityId(int facilityId);


    }
}
