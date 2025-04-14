using HotelApp.Core.ModelsReusability;

namespace HotelApp.Core.InterfacesReusability
{
    public interface ICarRentalService
    {
        Task<List<CarRentalModel>> GetAllAsync();

        Task AddRentalAsync(CarRentalModel rental);
    }
}
