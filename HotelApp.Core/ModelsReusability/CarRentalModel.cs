namespace HotelApp.Core.ModelsReusability
{
    public class CarRentalModel
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;

        public decimal BasePricePerDay { get; set; }

        public double? DiscountPercentage { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string? Description { get; set; }
    }

}
