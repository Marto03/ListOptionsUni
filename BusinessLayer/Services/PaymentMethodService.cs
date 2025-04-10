using BusinessLayer.Services;
using DataLayer.Models;

namespace DataLayer.Services
{
    public class PaymentMethodService : BaseService
    {
        public PaymentMethodService(HotelDbContextModel context) : base(context) { }

        public List<PaymentMethodModel> GetPaymentMethods()
        {
            return _context.PaymentMethods.ToList();
        }

        public void AddPaymentMethod(string name)
        {
            if (_context.PaymentMethods.AsEnumerable().Any(p => p.Type.ToString().ToLower().Trim() == name.ToLower().Trim() || p.Name.ToLower().Trim() == name.ToLower().Trim()))
            {
                throw new InvalidOperationException("Payment method already exists.");
            }

            // Ако няма такъв начин на плащане , го запазваме като потребителско добавено (Name)
            var amenity = new PaymentMethodModel { Name = name };
            //_context.PaymentMethods.Add(amenity);
            _context.Add(amenity);
            _context.SaveChanges();
        }
    }
}
