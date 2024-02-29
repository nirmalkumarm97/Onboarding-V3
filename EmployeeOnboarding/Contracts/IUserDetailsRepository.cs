using EmployeeOnboarding.Request;
using EmployeeOnboarding.Response;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Contracts
{
    public interface IUserDetailsRepository 
    {
     Task<int> AddPersonalInfo(bool directAdd , PersonalInfoRequest personalInfoRequest);
     Task<PersonalInfoResponse> GetPersonalInfo(int genId);
     Task<string>GetStatusByLoginId(int loginId);
    }
}
