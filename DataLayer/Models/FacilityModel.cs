namespace DataLayer.Models
{
    public class FacilityModel
    {
        public int Id { get; set; }
        public FacilityTypeEnum? Type { get; set; }  // Име на удобството (басейн, фитнес и т.н.) -> `enum`, но може да е null
        public string? Name { get; set; }  // Само ако Type е null (потребителски добавени)

        public bool? IsCustomAdded { get; set; }

        //    Ако Type != null → това е системно удобство(Pool, Gym, Spa).
        //    Ако Type == null → това е потребителско удобство(например „Йога зала“).
    }
}
