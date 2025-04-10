using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelDbContextModel _context;

        public HotelRepository(HotelDbContextModel context)
        {
            _context = context;
        }

        public void AddHotel(HotelModel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }

        public List<HotelModel> GetAllHotels()
        {
            return _context.Hotels.ToList();
        }
    }

}
