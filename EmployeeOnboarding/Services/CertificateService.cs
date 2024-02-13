using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.ViewModels;
using Microsoft.EntityFrameworkCore;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Services
{

    public class CertificateService
    {

        private readonly ApplicationDbContext _context;
        public CertificateService(ApplicationDbContext context)
        {
            _context = context;
        }

        private string SaveCertificateFileAsync(string certificateBase64, string empId, string fileName)
        {
            if (string.IsNullOrEmpty(certificateBase64))
            {
                return null; // Return null if no certificate bytes are provided
            }

            var certificateBytes = Convert.FromBase64String(certificateBase64);

            var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Documents", empId);
            if (!Directory.Exists(empFolderPath))
            {
                Directory.CreateDirectory(empFolderPath);
            }

            var filePath = Path.Combine(empFolderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.WriteAsync(certificateBytes, 0, certificateBytes.Length);
            }

            return filePath; // Return the file path
        }


        int index1 = 1; // Initialize the Company_no sequence

        public async Task<List<EmployeeCertifications>> AddCertificate(int empId, List<CertificateVM> certificates)
        {
            List<EmployeeCertifications> certificateVMs = new List<EmployeeCertifications>();
            foreach (var certificate in certificates)
            {
                var existingCertificate = _context.EmployeeCertifications.FirstOrDefault(e => e.EmpGen_Id == empId && e.Certificate_no == index1);

                if (existingCertificate != null)
                {
                    var certificateFileName = $"certificate{index1}.pdf"; // Generate the certificate filename
                    // Update existing record
                    existingCertificate.Certificate_name = certificate.Certificate_name;
                    existingCertificate.Issued_by = certificate.Issued_by;
                    DateOnly Valid_till = DateOnly.Parse(certificate.Valid_till);
                    existingCertificate.Valid_till = Valid_till;
                    existingCertificate.Duration = certificate.Duration;
                    existingCertificate.Percentage = certificate.Percentage;
                    existingCertificate.proof = SaveCertificateFileAsync(certificate.proof, empId.ToString(), certificateFileName);
                    existingCertificate.Date_Modified = DateTime.UtcNow;
                    existingCertificate.Modified_by = empId.ToString();
                    existingCertificate.Status = "A";

                }
                else
                {
                    // Add new record
                    var certificateFileName = $"certificate{index1}.pdf"; // Generate the certificate filename
                    var _certificate = new EmployeeCertifications()
                    {
                        EmpGen_Id = empId,
                        Certificate_no = index1,
                        Certificate_name = certificate.Certificate_name,
                        Issued_by = certificate.Issued_by,
                        Valid_till = DateOnly.Parse(certificate.Valid_till),
                        Duration = certificate.Duration,
                        Percentage = certificate.Percentage,
                        proof = SaveCertificateFileAsync(certificate.proof, empId.ToString(), certificateFileName),
                        Date_Created = DateTime.UtcNow,
                        Date_Modified = DateTime.UtcNow,
                        Created_by = empId.ToString(),
                        Modified_by = empId.ToString(),
                        Status = "A"
                    };
                    certificateVMs.Add(_certificate);

                }

                index1++;

            }
            _context.EmployeeCertifications.AddRange(certificateVMs);
            _context.SaveChanges();
            var id = certificateVMs.Select(x => x.EmpGen_Id).FirstOrDefault();

            return certificateVMs;
        }


        public List<getCertificateVM> GetCertificate(int empId)
        {
            var certificate = _context.EmployeeCertifications.Where(e => e.EmpGen_Id == empId && e.Certificate_no != null).Select(e => new getCertificateVM
            {
                Certificate_name = e.Certificate_name,
                Issued_by = e.Issued_by,
                Valid_till = e.Valid_till,
                Duration = e.Duration,
                Percentage = e.Percentage,
                proof = e.proof,
            })
            .ToList();

            return certificate;
        }

    }
}
