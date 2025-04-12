namespace HotelApp.Core.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public int PaymentMethodId { get; set; }  // FK към метода на плащане
        public int HotelId { get; set; }

        public virtual PaymentMethodModel PaymentMethod { get; set; }  // Навигационно свойство
    }
}
