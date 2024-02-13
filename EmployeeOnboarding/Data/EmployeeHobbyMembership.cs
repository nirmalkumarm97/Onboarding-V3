using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Data
{
    public class EmployeeHobbyMembership
    {
        public int Id { get; set; }
        [ForeignKey("EmpGen_Id")]
        public int EmpGen_Id { get; set; }  
        public bool ProfessionalBody { get; set; }
        public string? ProfessionalBody_name { get; set; }
        public string? Hobbies { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Modified { get; set; }
        public string Created_by { get; set; }
        public string? Modified_by { get; set; }
        public string Status { get; set; }
    }
}
