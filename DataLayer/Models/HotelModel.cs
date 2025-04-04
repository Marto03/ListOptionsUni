using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class HotelModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<FacilityModel> Facilities { get; set; } = new();
        // Всички резервации за този хотел
        public List<ReservationModel> Reservations { get; set; } = new List<ReservationModel>();

        // Всички плащания в този хотел
        public List<PaymentModel> Payments { get; set; } = new List<PaymentModel>();

    }
}
