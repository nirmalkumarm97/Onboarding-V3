namespace EmployeeOnboarding.ViewModels
{
    public class HealthVM
    {

        public string? Specific_health_condition { get; set; }
        public string? Allergies { get; set; }
        public bool surgery { get; set; }
        public string? Surgery_explaination { get; set; }
        public bool Night_shifts { get; set; }
        public bool Disability { get; set; }
        public string? Disability_explanation { get; set; }
        public int CovidVaccine { get; set; }
        public IFormFile Vaccine_certificate { get; set; }
        public IFormFile? Health_documents { get; set; }



    }
}
