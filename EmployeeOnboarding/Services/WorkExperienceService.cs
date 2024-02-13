using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.ViewModels;
using Microsoft.EntityFrameworkCore;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Services
{

    public class WorkExperienceService
    {

        private readonly ApplicationDbContext _context;
        public WorkExperienceService(ApplicationDbContext context)
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

        public async Task<List<EmployeeExperienceDetails>> AddExperiences(int empId, List<WorkExperienceVM> experiences)
        {
            List<EmployeeExperienceDetails> experienceVMs = new List<EmployeeExperienceDetails>();
            foreach (var experience in experiences)
            {
                var existingExperience = _context.EmployeeExperienceDetails.FirstOrDefault(e => e.EmpGen_Id == empId && e.Company_no == index1);

                if (existingExperience != null)
                {
                    var certificateFileName = $"experience{index1}.pdf"; // Generate the certificate filename
                    // Update existing record
                    existingExperience.Company_name = experience.Company_name;
                    existingExperience.Designation = experience.Designation;
                    DateOnly startDate = DateOnly.Parse(experience.StartDate);
                    DateOnly endDate = DateOnly.Parse(experience.EndDate);
                    existingExperience.StartDate = startDate;
                    existingExperience.EndDate = endDate;
                    existingExperience.Reporting_to = experience.Reporting_to;
                    existingExperience.Reason = experience.Reason;
                    existingExperience.Location = experience.Location;
                    existingExperience.Exp_Certificate = SaveCertificateFileAsync(experience.Exp_Certificate, empId.ToString(), certificateFileName);
                    existingExperience.Date_Modified = DateTime.UtcNow;
                    existingExperience.Modified_by = empId.ToString();
                    existingExperience.Status = "A";
                }
                else
                {
                    // Add new record
                    var certificateFileName = $"experience{index1}.pdf"; // Generate the certificate filename
                    var _experience= new EmployeeExperienceDetails()
                    {
                        EmpGen_Id = empId,
                        Company_no = index1,
                        Company_name = experience.Company_name,
                        Designation = experience.Designation,
                        // Parse and assign DateOnly values
                        StartDate = DateOnly.Parse(experience.StartDate),
                        EndDate = DateOnly.Parse(experience.EndDate),
                        Reporting_to = experience.Reporting_to,
                        Reason = experience.Reason,
                        Location = experience.Location,
                        Exp_Certificate =  SaveCertificateFileAsync(experience.Exp_Certificate, empId.ToString(), certificateFileName),
                        Date_Created = DateTime.UtcNow,
                        Date_Modified = DateTime.UtcNow,
                        Created_by = empId.ToString(),
                        Modified_by = empId.ToString(),
                        Status = "A"
                    };
                    experienceVMs.Add(_experience);  
                    
                }
              
                index1++;

            }
             _context.EmployeeExperienceDetails.AddRange(experienceVMs);
             _context.SaveChanges();
            //pending status
            var _onboard = new ApprovalStatus()
                        {
                            EmpGen_Id = empId,
                            Current_Status = (int)Status.Pending,
                            Comments = "",
                            Date_Created = DateTime.UtcNow,
                            Date_Modified = DateTime.UtcNow,
                            Created_by = empId.ToString(),
                            Modified_by = "Admin",
                            Status = "A",
                        };
                        _context.ApprovalStatus.Add(_onboard);
                        _context.SaveChanges();

            var id = experienceVMs.Select(x => x.EmpGen_Id).FirstOrDefault();

            return experienceVMs;

        }


    public List<getExperienceVM> GetCompanyByEmpId(int empId)
        {
            var companyExperiences = _context.EmployeeExperienceDetails.Where(e => e.EmpGen_Id == empId && e.Company_no != null).Select(e => new getExperienceVM
                {
                    Company_name = e.Company_name,
                    Designation = e.Designation,
                    Reason = e.Reason,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate
                })
                .ToList();

            return companyExperiences;
        }

    }
}
