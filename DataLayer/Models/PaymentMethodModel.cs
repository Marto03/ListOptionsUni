namespace DataLayer.Models
{
    public class PaymentMethodModel
    {
        public int Id { get; set; }
        public PaymentMethodTypeEnum Type { get; set; }  // Visa, PayPal и др.
        public bool IsSystemDefined { get; set; }  // Дали е системен или добавен от потребител

        public string? Name { get; set; }  // Само ако Type е null (потребителски добавени)
    }
}
