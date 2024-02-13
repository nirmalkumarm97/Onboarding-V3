using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Data
{
    public class EmployeeFamilyDetails
    {
        public int Id { get; set; }
        [ForeignKey("EmpGen_Id")]
        public int EmpGen_Id { get; set; }
        public int Family_no { get; set; }
        public string Relationship { get; set; }
        public string Name { get; set; }
        public DateOnly DOB { get; set; }
        public string Occupation { get; set; }
        public long contact { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Modified { get; set; }
        public string Created_by { get; set; }
        public string? Modified_by { get; set; }
        public string Status { get; set; }
    }
}
