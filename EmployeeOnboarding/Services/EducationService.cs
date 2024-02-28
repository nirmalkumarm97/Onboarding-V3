using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.ViewModels;
using Microsoft.EntityFrameworkCore;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Services
{

    public class EducationService
    {

        private readonly ApplicationDbContext _context;
        private readonly IUserDetailsRepository _userDetailsRepository;

        public EducationService(ApplicationDbContext context, IUserDetailsRepository userDetailsRepository)
        {
            _context = context;
            _userDetailsRepository = userDetailsRepository;
        }

        private string SaveCertificateFileAsync(string certificateBase64, string genId, string fileName)
        {
            if (string.IsNullOrEmpty(certificateBase64))
            {
                return null; // Return null if no certificate bytes are provided
            }

            var certificateBytes = Convert.FromBase64String(certificateBase64);

            var empFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\A_Onboarding\\OnboardingFiles", genId);
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

        public async Task<List<EmployeeEducationDetails>> AddEducation(int genId, List<EducationVM> educations)
        {
            try
            {
                List<EmployeeEducationDetails> educationVMs = new List<EmployeeEducationDetails>();
                foreach (var education in educations)
                {
                    var existingEducation = _context.EmployeeEducationDetails.FirstOrDefault(e => e.EmpGen_Id == genId && e.Education_no == index1);

                    if (existingEducation != null)
                    {
                        var certificateFileName = $"education{index1}.pdf"; // Generate the certificate filename
                                                                            // Update existing record
                        existingEducation.Qualification = education.Qualification;
                        existingEducation.University = education.University;
                        existingEducation.Institution_name = education.Institution_name;
                        existingEducation.Degree_achieved = education.Degree_achieved;
                        existingEducation.specialization = education.specialization;
                        existingEducation.Passoutyear = education.Passoutyear;
                        existingEducation.Percentage = education.Percentage;
                        existingEducation.Edu_certificate = SaveCertificateFileAsync(education.Edu_certificate, genId.ToString(), certificateFileName);
                        existingEducation.Date_Modified = DateTime.UtcNow;
                        existingEducation.Modified_by = genId.ToString();
                        existingEducation.Status = "A";
                    }
                    else
                    {
                        // Add new record
                        var certificateFileName = $"education{index1}.pdf"; // Generate the certificate filename
                        var _education = new EmployeeEducationDetails()
                        {
                            EmpGen_Id = genId,
                            Education_no = index1,
                            Qualification = education.Qualification,
                            University = education.University,
                            Institution_name = education.Institution_name,
                            Degree_achieved = education.Degree_achieved,
                            specialization = education.specialization,
                            Passoutyear = education.Passoutyear,
                            Percentage = education.Percentage,
                            Edu_certificate = SaveCertificateFileAsync(education.Edu_certificate, genId.ToString(), certificateFileName),
                            Date_Created = DateTime.UtcNow,
                            Date_Modified = DateTime.UtcNow,
                            Created_by = genId.ToString(),
                            Modified_by = genId.ToString(),
                            Status = "A"
                        };
                        educationVMs.Add(_education);
                    }

                    index1++;

                }
                _context.EmployeeEducationDetails.AddRange(educationVMs);
                _context.SaveChanges();
                //var id = educationVMs.Select(x => x.EmpGen_Id).FirstOrDefault();

                return educationVMs;

            }
            catch(Exception ex)
            {
                throw;
            }
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

        public List<GetEducationVM> GetEducation(int genId)
        {
            var education = _context.EmployeeEducationDetails.Where(e => e.EmpGen_Id == genId && e.Education_no != null).Select(e => new GetEducationVM
            {
                GenId   = e.EmpGen_Id,
                Qualification = e.Qualification,
                University = e.University,
                Institution_name = e.Institution_name,
                Degree_achieved = e.Degree_achieved,
                specialization = e.specialization,
                Passoutyear = e.Passoutyear,
                Percentage = e.Percentage,
                Edu_certificate = GetFile(e.Edu_certificate)
            })
                .ToList();

            return education;
        }

    }
}
