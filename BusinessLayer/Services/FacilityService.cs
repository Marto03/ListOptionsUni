using BusinessLayer.Services;
using DataLayer.Models;

namespace DataLayer.Services
{
    public class FacilityService : BaseService
    {
        public FacilityService(HotelDbContextModel context) : base (context) 
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public List<FacilityModel> GetAllFacilities()
        {
            return _context.Facilities.ToList();  // Извлича всички удобства от базата
        }

        public void AddFacility(string name)
        {
            if (_context.Facilities.Any(f => f.Name.ToLower() == name.ToLower()))
            {
                throw new InvalidOperationException("Facility already exists.");
            }
            //if (Enum.TryParse(name, true, out FacilityTypeEnum type))
            //{
            //    // Ако името съвпада с енум стойност, създаваме FacilityModel
            //    var amenity = new FacilityModel { Type = type, Name = type.ToString() };
            //    _context.Add(amenity);
            //}
            else
            {
                // Ако името не съвпада с енум стойност, го запазваме като потребителско добавено (Name)
                var amenity = new FacilityModel { Name = name, IsCustomAdded = true };
                _context.Add(amenity);
            }
            //var amenity = new FacilityModel { Type = (FacilityTypeEnum)name.ToString() };
            //_context.Add(amenity);
            _context.SaveChanges();
        }
    }
}
