namespace HotelApp.Core.Models
{
    public class PaymentMethodModel
    {
        public int Id { get; set; }
        public PaymentMethodTypeEnum? Type { get; set; }  // Visa, PayPal и др.
        public string? Name { get; set; }
        public bool? IsSystemDefined { get; set; }  // Дали е системен или добавен от потребител

    }
}
