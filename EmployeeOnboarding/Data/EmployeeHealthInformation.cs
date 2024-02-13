using EmployeeOnboarding.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Data
{
    public class EmployeeHealthInformation
    {
        public int Id { get; set; }
        [ForeignKey("EmpGen_Id")]
        public int EmpGen_Id { get; set; }  
        public string? Specific_health_condition { get; set; }
        public string? Allergies { get; set; }
        public bool surgery { get; set; }
        public string? Surgery_explaination { get; set; }
        public bool Night_shifts { get; set; }
        public bool Disability { get; set; }
        public string? Disability_explanation { get; set; }
        public int CovidVaccine { get; set; }
        public string Vaccine_certificate { get; set; }
        public string? Health_documents { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Modified { get; set; }
        public string Created_by { get; set; }
        public string? Modified_by { get; set; }
        public string Status { get; set; }

    }
}
