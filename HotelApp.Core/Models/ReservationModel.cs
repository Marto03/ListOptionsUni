using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApp.Core.Models
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

        public List<int> UsedFacilities { get; set; } = new();

        public int PaymentId { get; set; }

        // Добавяне на навигационни свойства:
        public virtual HotelModel? Hotel { get; set; }  // Свързано с Hotel
        public virtual UserModel? User { get; set; }    // Свързано с User
        public virtual PaymentModel? Payment { get; set; } // Свързано с Payment
        public virtual List<FacilityModel?>? Facilities { get; set; }  // Свързано с Facilities (може да се използва за изтегляне на детайлите на удобствата)

        [NotMapped]
        public string? UsedFacilitiesAsString { get; set; }

    }
}
