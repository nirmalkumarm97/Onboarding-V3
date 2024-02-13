namespace EmployeeOnboarding.ViewModels
{
    public class RequiredVM
    {
        public IFormFile Aadhar { get; set; }
        public IFormFile Pan { get; set; }
        public IFormFile? Driving_license { get; set; }
        public IFormFile? Passport { get; set; }


    }
}
