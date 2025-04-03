using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HotelDbContextModel _context;

        public PaymentRepository(HotelDbContextModel context)
        {
            _context = context;
        }

        public void AddPayment(PaymentModel payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public List<PaymentModel> GetAllPayments()
        {
            return _context.Payments.Include(p => p.Hotel).Include(p => p.Reservation).ToList();
        }
    }
}
