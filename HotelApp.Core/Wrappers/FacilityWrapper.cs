using HotelApp.Core.Abstractions;
using HotelApp.Core.Models;

namespace HotelApp.Core.Wrappers
{
    public class FacilityWrapper : DiscountableItem
    {
        public FacilityWrapper(FacilityModel facility, HotelFacilityModel hotelFacility)
        {
            Id = facility.Id;
            Name = facility.Name ?? "Непознато";
            Price = hotelFacility.Price;
            DiscountPercentage = hotelFacility.DiscountPercentage;
            FromDate = hotelFacility.FromDate;
            ToDate = hotelFacility.ToDate;
        }
    }


}
