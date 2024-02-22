using EmployeeOnboarding.ViewModels;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Response
{
    public class PersonalInfoResponse
    {
        public int loginId { get; set; }
        public GetGeneralVM generalVM { get; set; }
        public List<ContactVM> contact { get; set; }
        public List<GetFamilyVM> families { get; set; }
        public HobbyVM hobby { get; set; }
        public List<ColleagueVM> colleagues { get; set; }
        public List<EmergencyContactVM> emergencies { get; set; }
    }
}
