using Common.DTOs;
using DataLayer.Models;

namespace BusinessLayer.Services
{
    public class HotelFacilityService : BaseService
    {
        public HotelFacilityService(HotelDbContextModel context) : base(context) { }

        public List<HotelFacilityModel> GetFacilitiesByHotelId(int? hotelId)
        {
            return _context.HotelFacilities
                           .Where(hf => hf.HotelId == hotelId)
                           .ToList();
        }
        public string? GetHotelNameByUserHotelId(int? hotelId)
        {
            if (hotelId == null) return null;

            return _context.Hotels
                .Where(h => h.Id == hotelId)
                .Select(h => h.Name)
                .FirstOrDefault();
        }

        public void ReplaceFacilitiesForHotel(int hotelId, List<HotelFacilityModel> newFacilities)
        {
            var existing = _context.HotelFacilities.Where(hf => hf.HotelId == hotelId);
            _context.HotelFacilities.RemoveRange(existing);

            foreach (var facility in newFacilities)
            {
                facility.HotelId = hotelId;
                _context.HotelFacilities.Add(facility);
            }

            _context.SaveChanges();
        }
        public List<HotelFacilityDTO> GetFacilitiesForHotel(int? hotelId)
        {
            return GetFacilitiesByHotelId(hotelId)
                .Select(hf => new HotelFacilityDTO
                {
                    FacilityId = hf.FacilityId,
                    Price = hf.Price,
                    Discount = hf.DiscountPercentage
                }).ToList();
        }

        public void SaveFacilitiesForHotel(int hotelId, List<HotelFacilityDTO> facilityDtos)
        {
            if (facilityDtos == null || facilityDtos.Any(f => f.FacilityId == null))
                throw new ArgumentException("Facility ID не може да бъде null.");

            var newEntities = facilityDtos.Select(dto => new HotelFacilityModel
            {
                HotelId = hotelId,
                FacilityId = dto?.FacilityId ?? 0,
                Price = dto?.Price ?? 0,
                DiscountPercentage = dto?.Discount ?? 0
            }).ToList();

            ReplaceFacilitiesForHotel(hotelId, newEntities);
        }
        public void RemoveAllFacilitiesForHotel(int hotelId)
        {
            var existing = _context.HotelFacilities.Where(hf => hf.HotelId == hotelId);
            _context.HotelFacilities.RemoveRange(existing);
            _context.SaveChanges();
        }

    }

}
