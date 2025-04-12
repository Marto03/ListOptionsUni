namespace HotelApp.Core.DTOs
{
    public class HotelFacilityDTO
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public decimal Price { get; set; }
        public double? Discount { get; set; }
        public bool IsSelected { get; set; }
    }

}
