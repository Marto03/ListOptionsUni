using HotelApp.Core.InterfacesReusability;
using HotelApp.Core.ModelsReusability;
using HotelApp.Core.Repositories;

namespace HotelApp.BusinessLayer.ServicesReusability
{
    internal class CarRentalService : ICarRentalService
    {
        private readonly IGenericRepository<CarRentalModel> _repository;

        public CarRentalService(IGenericRepository<CarRentalModel> repository)
        {
            _repository = repository;
        }

        public async Task<List<CarRentalModel>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task AddRentalAsync(CarRentalModel rental) => await _repository.AddAsync(rental);

    }
}
