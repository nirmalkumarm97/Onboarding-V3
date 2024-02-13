using EmployeeOnboarding.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Services
{
    public class CityService
    {

        private ApplicationDbContext _context;
        public CityService(ApplicationDbContext context)

        {
            _context = context;
        }

        public void AddCity(int Id, City city)

        {
            var existingcity = _context.City.FirstOrDefault(e => e.Id == Id && e.State_Id == 23);

            if (existingcity != null)
            {
                //Update existing record

                existingcity.State_Id = city.State_Id;
                existingcity.City_Name = city.City_Name;
                existingcity.Date_Created = DateTime.UtcNow;
                existingcity.Date_Modified = DateTime.UtcNow;
            }
            else
            {
                //Add new record

                var _city = new City()
                {
                    State_Id = city.State_Id,
                    City_Name = city.City_Name,
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                };
                _context.City.Add(_city);
                _context.SaveChanges();
            }
        }
        public City GetCity(int Id)
        {
            var _city = _context.City.Where(n => n.Id == Id && n.State_Id <= 36).Select(city=> new City()
            {
                State_Id = city.State_Id,
                City_Name = city.City_Name,
                Date_Created = DateTime.UtcNow,
                Date_Modified = DateTime.UtcNow,


            }).FirstOrDefault();
            return _city;
        }

    }
}
 
