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

        [ForeignKey("HotelId")]
        public HotelModel Hotel { get; set; }

        [Required]
        public UserModel UserId { get; set; }

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
        public PaymentModel Payment { get; set; }




        public int PaymentId { get; set; }  // FK към плащането
        public PaymentModel Payment { get; set; }  // Навигационно свойство

        public int HotelId { get; set; }  // FK към хотела
        public HotelModel Hotel { get; set; }  // Навигационно свойство

        public int RoomTypeId { get; set; }  // FK към типа стая
        public RoomTypeModel RoomType { get; set; }  // Навигационно свойство

        public int UserId { get; set; }  // FK към потребителя
        public UserModel User { get; set; }  // Навигационно свойство





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
