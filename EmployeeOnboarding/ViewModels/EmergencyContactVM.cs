using Microsoft.AspNetCore.Http;
namespace OnboardingWebsite.Models
{
    public class EmergencyContactVM
    {

        public string Relationship { get; set; }
        public string Relation_name { get; set; }
        public long Contact_number { get; set; }
        public string Contact_address { get; set; }
    }
}
