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
    public class HealthService
    {
        private readonly ApplicationDbContext _context;

        public HealthService(ApplicationDbContext context)
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

        public void AddHealth(int Id, HealthVM health)
        {
            var existingHealth = _context.EmployeeHealthInformation.FirstOrDefault(e => e.EmpGen_Id == Id);

            if (existingHealth != null)
            { 
                existingHealth.Specific_health_condition= health.Specific_health_condition;
                existingHealth.Allergies =health.Allergies;
                existingHealth.surgery = health.surgery;
                existingHealth.Surgery_explaination = health.Surgery_explaination;
                existingHealth.Night_shifts= health.Night_shifts;
                existingHealth.Disability= health.Disability;
                existingHealth.Disability_explanation= health.Disability_explanation;
                existingHealth.CovidVaccine = health.CovidVaccine;
                existingHealth.Vaccine_certificate = SaveCertificateFile(health.Vaccine_certificate, Id.ToString(), "Vacc_Certificate.pdf");
                existingHealth.Health_documents = SaveCertificateFile(health.Health_documents, Id.ToString(), "Health_Documents.pdf");
                existingHealth.Date_Modified = DateTime.UtcNow;
                existingHealth.Modified_by = Id.ToString();
                existingHealth.Status = "A";
            }
            else
            {
                // Add new record
                var _health = new EmployeeHealthInformation()
                {
                    EmpGen_Id= Id,
                    Specific_health_condition = health.Specific_health_condition,
                    Allergies = health.Allergies,
                    surgery = health.surgery,
                    Surgery_explaination = health.Surgery_explaination,
                    Night_shifts = health.Night_shifts,
                    Disability= health.Disability,
                    Disability_explanation= health.Disability_explanation,
                    CovidVaccine= health.CovidVaccine,
                    Vaccine_certificate= SaveCertificateFile(health.Vaccine_certificate, Id.ToString(), "Vacc_Certificate.pdf"),
                    Health_documents = SaveCertificateFile(health.Health_documents, Id.ToString(), "Health_Documents.pdf"),
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = Id.ToString(),
                    Modified_by = Id.ToString(),
                    Status = "A"
                };

                _context.EmployeeHealthInformation.Add(_health);
            }
            _context.SaveChanges();
        }

        public HealthVM GetHealth(int Id)
        {
            var _health = _context.EmployeeHealthInformation.Where(n => n.EmpGen_Id == Id).Select(health => new HealthVM()
            {
               Specific_health_condition = health.Specific_health_condition,
               Allergies = health.Allergies,
               surgery = health.surgery,
               Surgery_explaination=health.Surgery_explaination,
               Night_shifts = health.Night_shifts,
               Disability=health.Disability,
               Disability_explanation=health.Disability_explanation,
               CovidVaccine=health.CovidVaccine,
              
            }).FirstOrDefault();

            return _health;
        }
    }
}
