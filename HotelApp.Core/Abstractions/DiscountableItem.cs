using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Core.Abstractions
{
    public abstract class DiscountableItem : IItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public double? DiscountPercentage { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public virtual decimal GetFinalPrice(DateTime now)
        {
            bool isInDiscountPeriod =
                (!FromDate.HasValue || now >= FromDate) &&
                (!ToDate.HasValue || now <= ToDate);

            if (DiscountPercentage.HasValue && isInDiscountPeriod)
            {
                return Price - (Price * (decimal)(DiscountPercentage.Value / 100));
            }

            return Price;
        }
    }

}
