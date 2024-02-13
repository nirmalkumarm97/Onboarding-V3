using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace EmployeeOnboarding.Data
{
    public class EmployeeColleagueDetails
    {
        public int Id { get; set; }
        [ForeignKey("EmpGen_Id")]
        public int EmpGen_Id { get; set; }
        public int colleague_no { get; set; }
        public string? Employee_id { get; set; }
        public string? Colleague_Name { get; set; }
        public string? Location { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Modified { get; set; }
        public string Created_by { get; set; }
        public string? Modified_by { get; set; }
        public string Status { get; set; }

    }
}
//