using HotelApp.Core.Abstractions;
using HotelApp.Core.ModelsReusability;

namespace HotelApp.Core.Wrappers
{
    public class CarRentalWrapper : DiscountableItem
    {
        public CarRentalWrapper(CarRentalModel model)
        {
            Id = model.Id;
            Name = $"{model.Brand} {model.Model}";
            Price = model.BasePricePerDay;
            DiscountPercentage = model.DiscountPercentage;
            FromDate = model.FromDate;
            ToDate = model.ToDate;
        }
    }
}
