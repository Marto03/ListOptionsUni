//using DataLayer.Models;
//using DataLayer.Repositories;

//namespace BusinessLayer.Services
//{
//    public class PaymentService
//    {
//        private readonly IPaymentRepository _paymentRepository;

//        public PaymentService(IPaymentRepository paymentRepository)
//        {
//            _paymentRepository = paymentRepository;
//        }

//        public void AddPayment(PaymentModel payment)
//        {
//            if (payment.Amount <= 0)
//                throw new InvalidOperationException("Сумата на плащането трябва да е положителна!");

//            _paymentRepository.AddPayment(payment);
//        }

//        public List<PaymentModel> GetAllPayments()
//        {
//            return _paymentRepository.GetAllPayments();
//        }
//    }
//}
