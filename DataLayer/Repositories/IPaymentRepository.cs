using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IPaymentRepository
    {
        void AddPayment(PaymentModel payment);
        List<PaymentModel> GetAllPayments();
    }
}
