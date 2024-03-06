using EmployeeOnboarding.Data;
using EmployeeOnboarding.ViewModels;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Contracts
{
    public interface IUserRepository
    {
        Task<int>AddEducation(int genId, List<EducationVM> educations);
        List<GetEducationVM> GetEducation(int genId);
        Task<int> AddCertificate(int genId, List<CertificateVM> certificates);
        List<getCertificateVM> GetCertificate(int genId); 
        Task<int> AddExperiences(int genId, List<WorkExperienceVM> experiences);
        List<getExperienceVM> GetCompanyByEmpId(int genId);
        Task<int> AddReference(int genId, ReferenceVM reference);
        GetReferenceVM Getreference(int genId);
        Task<int> AddHealth(int genId, HealthVM health);
        GetHealthVM GetHealth(int Id);
        Task<int> AddBank(int genId, ExistingBankVM bank);
        GetExistingBankVM GetBank(int genId);
    }
}
