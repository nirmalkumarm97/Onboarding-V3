using Microsoft.AspNetCore.Http;
namespace OnboardingWebsite.Models
{
    public class CertificateVM
    {
        public string? Certificate_name { get; set; }
        public string? Issued_by { get; set; }
        public string? Valid_till { get; set; }
        public int? Duration { get; set; }
        public string? Percentage { get; set; }
        public string? proof { get; set; }
    }
}
