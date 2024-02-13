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
    public class ReferenceService
    {
        private ApplicationDbContext _context;

        public ReferenceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddReference(int Id, ReferenceVM reference)
        {
            var existingreference = _context.EmployeeReferenceDetails.FirstOrDefault(e => e.EmpGen_Id == Id);

            if (existingreference != null)
            {
                existingreference.Referral_name = existingreference.Referral_name;
                existingreference.Designation = existingreference.Designation;
                existingreference.Company_name = existingreference.Company_name;
                existingreference.Contact_number = existingreference.Contact_number;
                existingreference.Email_Id = existingreference.Email_Id;
                existingreference.Authorize = existingreference.Authorize;
                existingreference.Date_Modified = DateTime.UtcNow;
                existingreference.Modified_by = Id.ToString();
                existingreference.Status = "A";
            }
            else
            {
                // Add new record
                var _reference = new EmployeeReferenceDetails()
                {
                    EmpGen_Id = Id,
                    Referral_name = reference.Referral_name,
                    Designation = reference.Designation,
                    Company_name = reference.Company_name,
                    Contact_number = reference.Contact_number,
                    Email_Id = reference.Email_Id,
                    Authorize = reference.Authorize,
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = Id.ToString(),
                    Modified_by = Id.ToString(),
                    Status = "A"
                };

                _context.EmployeeReferenceDetails.Add(_reference);
            }
            _context.SaveChanges();
        }

        public ReferenceVM Getreference(int Id)
        {
            var _reference = _context.EmployeeReferenceDetails.Where(n => n.EmpGen_Id == Id).Select(reference => new ReferenceVM()
            {
                Referral_name = reference.Referral_name,
                Designation = reference.Designation,
                Company_name = reference.Company_name,
                Contact_number = reference.Contact_number,
                Email_Id = reference.Email_Id,
                Authorize = reference.Authorize

            }).FirstOrDefault();

            return _reference;
        }
    }
}
