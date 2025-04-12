using System.ComponentModel.DataAnnotations;

namespace HotelApp.Core.Models
{
    public class HotelModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        //public List<FacilityModel> Facilities { get; set; } = new();

    }
}
