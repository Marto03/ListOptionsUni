using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelDbContextModel _context;

        public ReservationRepository(HotelDbContextModel context)
        {
            _context = context;
        }

        public void AddReservation(ReservationModel reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public List<ReservationModel> GetAllReservations()
        {
            return _context.Reservations.Include(r => r.Hotel).Include(r => r.Payment).ToList();
        }
    }
}
