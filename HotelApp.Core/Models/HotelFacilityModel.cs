namespace HotelApp.Core.Models
{
    public class HotelFacilityModel
    {
        public int Id { get; set; }

        public int? HotelId { get; set; }

        public int FacilityId { get; set; }

        public decimal Price { get; set; }

        // Може да бъде null, ако няма отстъпка
        public double? DiscountPercentage { get; set; }
    }

}
