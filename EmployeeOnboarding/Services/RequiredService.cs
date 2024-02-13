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
    public class RequiredService
    {
        private ApplicationDbContext _context;

        public RequiredService(ApplicationDbContext context)
        {
            _context = context;
        }

        private string SaveCertificateFile(IFormFile certificateFile, string Id, string fileName)
        {
            if (certificateFile == null)
            {
                return null;
            }
            var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", Id);
            if (!Directory.Exists(empFolderPath))
            {
                Directory.CreateDirectory(empFolderPath);
            }
            var filePath = Path.Combine(empFolderPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                certificateFile.CopyTo(fileStream);
            }
            return filePath; // Return the file path
        }

        public void AddRequired(int Id, RequiredVM required)
        {
            var existingRequired = _context.EmployeeRequiredDocuments.FirstOrDefault(e => e.EmpGen_Id == Id);

            if (existingRequired != null)
            {
                existingRequired.Aadhar= SaveCertificateFile(required.Aadhar, Id.ToString(), "Aadhar.pdf");
                existingRequired.Pan = SaveCertificateFile(required.Pan, Id.ToString(), "Pan.pdf");
                existingRequired.Driving_license = SaveCertificateFile(required.Driving_license, Id.ToString(), "Driving_license.pdf");
                existingRequired.Passport = SaveCertificateFile(required.Passport, Id.ToString(), "Passport.pdf");
                existingRequired.Date_Modified = DateTime.UtcNow;
                existingRequired.Modified_by = Id.ToString();
                existingRequired.Status = "A";
            }
            else
            {
                // Add new record
                var _required = new EmployeeRequiredDocuments()
                {
                    Aadhar = SaveCertificateFile(required.Aadhar, Id.ToString(), "Aadhar.pdf"),
                    Pan = SaveCertificateFile(required.Pan, Id.ToString(), "Pan.pdf"),
                    Driving_license = SaveCertificateFile(required.Driving_license, Id.ToString(), "Driving_license.pdf"),
                    Passport = SaveCertificateFile(required.Passport, Id.ToString(), "Passport.pdf"),
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = Id.ToString(),
                    Modified_by = Id.ToString(),
                    Status = "A"
                };

                _context.EmployeeRequiredDocuments.Add(_required);
            }
            _context.SaveChanges();
        }
    }
}
