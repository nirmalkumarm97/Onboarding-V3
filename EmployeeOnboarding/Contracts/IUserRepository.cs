using EmployeeOnboarding.Data;
using EmployeeOnboarding.ViewModels;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Contracts
{
    public interface IUserRepository
    {
        Task<string>AddEducation(int genId, List<EducationVM> educations);
        List<GetEducationVM> GetEducation(int genId);
        Task<string> AddCertificate(int genId, List<CertificateVM> certificates);
        List<getCertificateVM> GetCertificate(int genId); 
        Task<List<EmployeeExperienceDetails>> AddExperiences(int genId, List<WorkExperienceVM> experiences);
        List<getExperienceVM> GetCompanyByEmpId(int genId);
        Task<string> AddReference(int genId, ReferenceVM reference);
        GetReferenceVM Getreference(int genId);
        Task<string> AddHealth(int genId, HealthVM health);
        GetHealthVM GetHealth(int Id);
        Task<string> AddBank(int genId, ExistingBankVM bank);
        GetExistingBankVM GetBank(int genId);






    }
}
