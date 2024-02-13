using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Data
{
    public class EmployeeEmergencyContactDetails
    {
        public int Id { get; set; }
        [ForeignKey("EmpGen_Id")]
        public int EmpGen_Id { get; set; }
        public int? emergency_no {get; set; }
        public string Relationship { get; set; }
        public string Relation_name { get; set; }
        public long Contact_number { get; set; }
        public string Contact_address { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Modified { get; set; }
        public string Created_by { get; set; }
        public string? Modified_by { get; set; }
        public string Status { get; set; }
    }
}
