using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using HotelApp.Core.Repositories;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("DatabaseConfig")]

namespace HotelApp.BusinessLayer.Services
{
    internal class FacilityService : IFacilityService
    {
        private readonly IGenericRepository<FacilityModel> _facilityRepository;

        public FacilityService(IGenericRepository<FacilityModel> facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        public List<FacilityModel> GetAllFacilities()
        {
            return _facilityRepository.GetAll();
        }

        public FacilityModel AddFacility(string name)
        {
            // Проверка дали съществува такова удобство
            if (_facilityRepository.GetAll().Any(f => f.Name.ToLower() == name.ToLower()))
                throw new InvalidOperationException("Facility already exists.");

            var facility = new FacilityModel { Name = name, IsCustomAdded = true };

            _facilityRepository.Add(facility); // Добавяне чрез GenericRepository
            return facility;
        }

        public void RemoveFacility(FacilityModel facility)
        {
            _facilityRepository.Delete(facility.Id); // Изтриване чрез GenericRepository
        }
    }
}
