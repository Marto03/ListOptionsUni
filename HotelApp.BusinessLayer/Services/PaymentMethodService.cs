using HotelApp.Core.Interfaces;
using HotelApp.Core.Models;
using HotelApp.Core.Repositories;

namespace HotelApp.BusinessLayer.Services
{
    internal class PaymentMethodService : IPaymentMethodService
    {
        private readonly IGenericRepository<PaymentMethodModel> _paymentMethodRepository;

        public PaymentMethodService(IGenericRepository<PaymentMethodModel> paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        // Получаване на всички методи на плащане
        public List<PaymentMethodModel> GetPaymentMethods()
        {
            return _paymentMethodRepository.GetAll().ToList(); // Използваме обобщеното репозиториум
        }

        // Добавяне на нов метод за плащане
        public void AddPaymentMethod(string name)
        {
            if (_paymentMethodRepository.GetAll().Any(p => p.Name.ToLower().Trim() == name.ToLower().Trim()))
            {
                throw new InvalidOperationException("Payment method already exists.");
            }

            // Създаваме нов метод на плащане
            var paymentMethod = new PaymentMethodModel { Name = name };
            _paymentMethodRepository.Add(paymentMethod); // Използваме метода Add от репозиториума
        }
    }

}
