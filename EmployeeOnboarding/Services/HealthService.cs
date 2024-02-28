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
using EmployeeOnboarding.Contracts;

namespace EmployeeOnboarding.Services
{
    public class HealthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserDetailsRepository _userDetailsRepository;

        public HealthService(ApplicationDbContext context, IUserDetailsRepository userDetailsRepository)
        {
            _context = context;
            _userDetailsRepository = userDetailsRepository;
        }

        private string SaveCertificateFile(string certificateBase64, string Id, string fileName)
        {
            if (certificateBase64 == null)
            {
                return null;
            }
            var certificateBytes = Convert.FromBase64String(certificateBase64);

            var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\A_Onboarding\\OnboardingFiles", Id);
            if (!Directory.Exists(empFolderPath))
            {
                Directory.CreateDirectory(empFolderPath);
            }
            var filePath = Path.Combine(empFolderPath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                // certificateFile.CopyTo(fileStream);
                fileStream.WriteAsync(certificateBytes, 0, certificateBytes.Length);

            }
            return filePath; // Return the file path
        }

        public void AddHealth(int genId, HealthVM health)
        {
            var existingHealth = _context.EmployeeHealthInformation.FirstOrDefault(e => e.EmpGen_Id == genId);

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
                existingHealth.Vaccine_certificate = SaveCertificateFile(health.Vaccine_certificate, genId.ToString(), "Vacc_Certificate.pdf");
                existingHealth.Health_documents = SaveCertificateFile(health.Health_documents, genId.ToString(), "Health_Documents.pdf");
                existingHealth.Date_Modified = DateTime.UtcNow;
                existingHealth.Modified_by = genId.ToString();
                existingHealth.Status = "A";
            }
            else
            {
                // Add new record
                var _health = new EmployeeHealthInformation()
                {
                    EmpGen_Id= genId,
                    Specific_health_condition = health.Specific_health_condition,
                    Allergies = health.Allergies,
                    surgery = health.surgery,
                    Surgery_explaination = health.Surgery_explaination,
                    Night_shifts = health.Night_shifts,
                    Disability= health.Disability,
                    Disability_explanation= health.Disability_explanation,
                    CovidVaccine= health.CovidVaccine,
                    Vaccine_certificate= SaveCertificateFile(health.Vaccine_certificate, genId.ToString(), "Vacc_Certificate.pdf"),
                    Health_documents = SaveCertificateFile(health.Health_documents, genId.ToString(), "Health_Documents.pdf"),
                    Date_Created = DateTime.UtcNow,
                    Date_Modified = DateTime.UtcNow,
                    Created_by = genId.ToString(),
                    Modified_by = genId.ToString(),
                    Status = "A"
                };

                _context.EmployeeHealthInformation.Add(_health);
            }
            _context.SaveChanges();
        }
        private static byte[] GetFile(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                System.IO.FileStream fs = System.IO.File.OpenRead(filepath);
                byte[] file = new byte[fs.Length];
                int br = fs.Read(file, 0, file.Length);
                if (br != fs.Length)
                {
                    throw new IOException("Invalid path");
                }
                return file;
            }
            return null;
        }

        public GetHealthVM GetHealth(int Id)
        {
            var _health = _context.EmployeeHealthInformation.Where(n => n.EmpGen_Id == Id).Select(health => new GetHealthVM()
            {
               GenId = health.EmpGen_Id,
               Specific_health_condition = health.Specific_health_condition,
               Allergies = health.Allergies,
               surgery = health.surgery,
               Surgery_explaination=health.Surgery_explaination,
               Night_shifts = health.Night_shifts,
               Disability=health.Disability,
               Disability_explanation=health.Disability_explanation,
               CovidVaccine=health.CovidVaccine,
               Health_documents = GetFile(health.Health_documents),
               Vaccine_certificate = GetFile(health.Vaccine_certificate)
            }).FirstOrDefault();

            return _health;
        }
    }
}
