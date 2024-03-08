using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnboardingWebsite.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Request
{
    public class PersonalInfoRequest
    {
        public int loginId { get; set; }
        public int? GenId { get; set; }
        public GeneralInfo generalVM { get; set; }
        public List<ContactInfo> contact { get; set; }
        public List<FamilyInfo> families { get; set; }
        public HobbiesInfo hobby { get; set; }
        public List<ColleagueInfo> colleagues { get; set; }
        public List<EmergencyContactInfo> emergencies { get; set; }
        public RequiredInfo RequiredDocuments { get; set; }
    }
}
