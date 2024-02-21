using EmployeeOnboarding.Request;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Contracts
{
    public interface IUserDetailsRepository 
    {
     Task<bool> AddPersonalInfo(PersonalInfoRequest personalInfoRequest);

    }
}
