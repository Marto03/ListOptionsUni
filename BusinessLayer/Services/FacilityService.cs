using BusinessLayer.Services;
using DataLayer.Models;

namespace DataLayer.Services
{
    public class FacilityService : BaseService
    {
        public FacilityService(HotelDbContextModel context) : base(context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public List<FacilityModel> GetAllFacilities()
        {
            return _context.Facilities.ToList();  // Извлича всички удобства от базата
        }

        public FacilityModel AddFacility(string name)
        {
            if (_context.Facilities.Any(f => f.Name.ToLower() == name.ToLower()))
                throw new InvalidOperationException("Facility already exists.");

            // Ако името не съвпада с енум стойност, го запазваме като потребителско добавено (Name)
            var facility = new FacilityModel { Name = name, IsCustomAdded = true };
            _context.Facilities.Add(facility);

            _context.SaveChanges();
            return _context.Facilities.FirstOrDefault(f => f.Name == name);
        }
        public void RemoveFacility(FacilityModel facility)
        {
            var existingFacility = _context.Facilities.Find(facility.Id);

            if (existingFacility != null)
            {
                _context.Remove(facility);
                _context.SaveChanges();
            }
        }
    }
}
