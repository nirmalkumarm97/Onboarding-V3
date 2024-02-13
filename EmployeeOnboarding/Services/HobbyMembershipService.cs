using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Add this namespace for IFormFile
using System.IO; // Add this namespace for file operations
using System.Threading.Tasks;

namespace EmployeeOnboarding.Services
{
    public class HobbyMembershipService
    {
        private ApplicationDbContext _context;

        public HobbyMembershipService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddHobby(int Id, HobbyVM hobby)
        {
            var existingHobby = _context.EmployeeHobbyMembership.FirstOrDefault(e => e.EmpGen_Id == Id);

            if (existingHobby != null)
            {
                existingHobby.ProfessionalBody = hobby.ProfessionalBody;
                existingHobby.ProfessionalBody_name = hobby.ProfessionalBody_name;
                existingHobby.Hobbies = hobby.Hobbies;
                existingHobby.Date_Modified = DateTime.UtcNow;
                existingHobby.Modified_by = Id.ToString();
                existingHobby.Status = "A";
            }
            else
            {
                // Add new record
                var _hobby = new EmployeeHobbyMembership()
                {
                    EmpGen_Id = Id,
                    ProfessionalBody = hobby.ProfessionalBody,
                    ProfessionalBody_name = hobby.ProfessionalBody_name,
                    Hobbies = hobby.Hobbies,
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = Id.ToString(),
                    Modified_by = Id.ToString(),
                    Status = "A"
                };

                _context.EmployeeHobbyMembership.Add(_hobby);
            }
            _context.SaveChanges();
        }

        public HobbyVM GetHobby(int Id)
        {
            var _hobby = _context.EmployeeHobbyMembership.Where(n => n.EmpGen_Id == Id).Select(hobby => new HobbyVM()
            {
                ProfessionalBody = hobby.ProfessionalBody,
                ProfessionalBody_name = hobby.ProfessionalBody_name,
                Hobbies = hobby.Hobbies,

            }).FirstOrDefault();

            return _hobby;
        }
    }
}
