namespace Common.DTOs
{
    public class FacilityDTO
    {
        public string Name { get; set; }
        public bool IsSystemDefined { get; set; }


        public static readonly List<FacilityDTO> DefaultFacilities = new List<FacilityDTO>
        {
            new FacilityDTO { Name = "Басейн", IsSystemDefined = true },
            new FacilityDTO { Name = "Фитнес", IsSystemDefined = true },
            new FacilityDTO { Name = "СПА", IsSystemDefined = true }
        };
    }
}
