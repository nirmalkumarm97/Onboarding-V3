using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Request
{
    public class PersonalInfoRequest
    {
        public int loginId { get; set; }
        public GeneralVM generalVM { get; set; }
        public List<ContactVM> contact { get; set; }
        public List<FamilyVM>? families { get; set; }
        public HobbyVM hobby { get; set; }
        public List<ColleagueVM>? colleagues { get; set; }
        public List<EmergencyContactVM>? emergencies { get; set; }
        public  RequiredVM required { get; set; }

    }
}
