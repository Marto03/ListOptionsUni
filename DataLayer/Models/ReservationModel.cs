using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class ReservationModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }


        [Required]
        public int UserId { get; set; }

        [Required]
        public RoomTypeEnum RoomType { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        public List<FacilityModel> UsedFacilities { get; set; } = new();

        public int PaymentId { get; set; }




        public void CalculateFinalPrice()
        {
            decimal discount = 0;
            if (UsedFacilities.Any(f => f.Type == null || f.Type == FacilityTypeEnum.Pool))
            {
                discount = 0.10m; // 10% намаление
            }
            Price -= Price * discount;
        }
    }
}
