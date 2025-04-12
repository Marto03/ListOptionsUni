using HotelApp.Core.Models;

namespace HotelApp.Core.Interfaces
{
    public interface IPaymentMethodService
    {
        List<PaymentMethodModel> GetPaymentMethods();

        void AddPaymentMethod(string name);
        
    }
}
