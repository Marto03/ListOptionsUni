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
            if (_context.PaymentMethods.Any(p => p.Type.ToString() == name || p.Name == name))
            {
                throw new InvalidOperationException("Payment method already exists.");
            }
            if (Enum.TryParse(name, true, out PaymentMethodTypeEnum type))
            {
                // Ако името съвпада с енум стойност, създаваме FacilityModel
                var amenity = new PaymentMethodModel { Type = type , Name = type.ToString() , IsSystemDefined = true};
                _context.Add(amenity);
            }
            else
            {
                // Ако името не съвпада с енум стойност, го запазваме като потребителско добавено (Name)
                var amenity = new PaymentMethodModel { Name = name , IsSystemDefined = false};
                _context.Add(amenity);
            }
            _context.SaveChanges();
        }
    }
}
