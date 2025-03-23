using DataLayer.Models;

namespace BusinessLayer.Services
{
    public abstract class BaseService
    {
        protected readonly HotelDbContextModel _context;

        protected BaseService(HotelDbContextModel context)
        {
            _context = context;
        }

    }
}
