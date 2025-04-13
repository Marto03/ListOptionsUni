using HotelApp.Core.DTOs;
using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using HotelApp.Core.Repositories;

namespace HotelApp.BusinessLayer.Services
{
    internal class HotelFacilityService : IHotelFacilityService
    {
        private readonly IGenericRepository<HotelFacilityModel> _hotelFacilityRepository;
        private readonly IGenericRepository<HotelModel> _hotelRepository;
        private readonly IGenericRepository<FacilityModel> _facilityRepository;

        public HotelFacilityService(
            IGenericRepository<HotelFacilityModel> hotelFacilityRepository,
            IGenericRepository<HotelModel> hotelRepository,
            IGenericRepository<FacilityModel> facilityRepository)
        {
            _hotelFacilityRepository = hotelFacilityRepository;
            _hotelRepository = hotelRepository;
            _facilityRepository = facilityRepository;
        }

        public List<HotelFacilityModel> GetFacilitiesByHotelId(int? hotelId)
        {
            // Използване на GenericRepository за получаване на съществуващи записи
            return _hotelFacilityRepository.GetAll()
                .Where(hf => hf.HotelId == hotelId)
                .ToList();
        }

        public string? GetHotelNameByUserHotelId(int? hotelId)
        {
            if (hotelId == null) return null;

            // Използване на GenericRepository за работа с хотели
            return _hotelRepository.GetAll()
                .Where(h => h.Id == hotelId)
                .Select(h => h.Name)
                .FirstOrDefault();
        }

        public void ReplaceFacilitiesForHotel(int hotelId, List<HotelFacilityModel> newFacilities)
        {
            // Изтриваме старите удобства за хотела
            var existing = _hotelFacilityRepository.GetAll()
                .Where(hf => hf.HotelId == hotelId);
            foreach (var item in existing)
            {
                _hotelFacilityRepository.Delete(item.Id);
            }

            // Добавяме новите удобства
            foreach (var facility in newFacilities)
            {
                facility.HotelId = hotelId;
                _hotelFacilityRepository.Add(facility);
            }
        }

        public List<HotelFacilityDTO> GetFacilitiesForHotel(int? hotelId)
        {
            var hotelFacilities = GetFacilitiesByHotelId(hotelId);
            var facilities = _facilityRepository.GetAll()
                .ToDictionary(f => f.Id, f => f.Name);

            return hotelFacilities.Select(hf => new HotelFacilityDTO
            {
                FacilityId = hf.FacilityId,
                FacilityName = facilities.ContainsKey(hf.FacilityId) ? facilities[hf.FacilityId] : "Непознато",
                Price = hf.Price,
                Discount = hf.DiscountPercentage ?? 0
            }).ToList();
        }

        public void SaveFacilitiesForHotel(int hotelId, List<HotelFacilityDTO> facilityDtos)
        {
            if (facilityDtos == null || facilityDtos.Any(f => f.FacilityId == null))
                return;

            var newEntities = facilityDtos.Select(dto => new HotelFacilityModel
            {
                HotelId = hotelId,
                FacilityId = dto.FacilityId,
                Price = dto.Price,
                DiscountPercentage = dto.Discount ?? 0,
                FromDate = dto.FromDate,
                ToDate = dto.ToDate
            }).ToList();

            ReplaceFacilitiesForHotel(hotelId, newEntities);
        }

        public void RemoveAllFacilitiesForHotel(int hotelId)
        {
            var existing = _hotelFacilityRepository.GetAll()
                .Where(hf => hf.HotelId == hotelId);
            foreach (var item in existing)
            {
                _hotelFacilityRepository.Delete(item.Id);
            }
        }
        public List<HotelModel> GetHotelsByFacilityId(int facilityId)
        {
            // понеже нямам навигационно свойство Hotel в HotelFacilityModel
            var hotelIds = _hotelFacilityRepository.GetAll().Where(hf => hf.FacilityId == facilityId).Select(hf=>hf.HotelId).Distinct().ToList();
            return _hotelRepository.GetAll()
                .Where(h => hotelIds.Contains(h.Id))
                .ToList();
        }
    }

}
