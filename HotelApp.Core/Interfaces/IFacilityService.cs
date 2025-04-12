using HotelApp.Core.Models;

namespace HotelApp.Core.Interfaces
{
    public interface IFacilityService
    {
        List<FacilityModel> GetAllFacilities();

        FacilityModel AddFacility(string name);

        void RemoveFacility(FacilityModel facility);
        
    }
}
