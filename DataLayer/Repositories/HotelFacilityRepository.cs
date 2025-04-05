//using DataLayer.Models;

//namespace DataLayer.Repositories
//{
//    public class HotelFacilityRepository : IHotelFacilityRepository
//    {
//        private readonly HotelDbContextModel _context;

//        public HotelFacilityRepository(HotelDbContextModel context)
//        {
//            _context = context;
//        }

//        public List<HotelFacilityModel> GetFacilitiesByHotelId(int hotelId)
//        {
//            return _context.HotelFacilities
//                           .Where(hf => hf.HotelId == hotelId)
//                           .ToList();
//        }

//        public void ReplaceFacilitiesForHotel(int hotelId, List<HotelFacilityModel> newFacilities)
//        {
//            var existing = _context.HotelFacilities.Where(hf => hf.HotelId == hotelId);
//            _context.HotelFacilities.RemoveRange(existing);

//            foreach (var facility in newFacilities)
//            {
//                facility.HotelId = hotelId;
//                _context.HotelFacilities.Add(facility);
//            }

//            _context.SaveChanges();
//        }
//    }
//}
