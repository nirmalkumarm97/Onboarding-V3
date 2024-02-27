using EmployeeOnboarding.Request;
using EmployeeOnboarding.Response;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Contracts
{
    public interface IUserDetailsRepository 
    {
     Task<int> AddPersonalInfo(PersonalInfoRequest personalInfoRequest);
     Task<PersonalInfoResponse> GetPersonalInfo(int Id);
     Task<string>GetStatusByLoginId(int loginId);
    }
}
