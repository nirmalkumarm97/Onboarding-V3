using Microsoft.AspNetCore.Http;
namespace OnboardingWebsite.Models
{
    public class EducationVM
    {
        public string Qualification { get; set; }
        public string University { get; set; }
        public string Institution_name { get; set; }
        public string Degree_achieved { get; set; }
        public string specialization { get; set; }
        public int Passoutyear { get; set; }
        public string Percentage { get; set; }
        public string Edu_certificate { get; set; }
    }
}
