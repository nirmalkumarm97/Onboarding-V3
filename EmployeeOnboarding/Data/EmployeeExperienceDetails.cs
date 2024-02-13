using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Data
{
    public class EmployeeExperienceDetails
    {
        public int Id { get; set; }
        [ForeignKey("EmpGen_Id")]
        public int? EmpGen_Id { get; set; }
        public int? Company_no { get; set; }
        public string? Company_name { get; set; }
        public string? Designation { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Reporting_to { get; set; }
        public string? Reason { get; set; }
        public string? Location { get; set; }
        public string? Exp_Certificate { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Modified { get; set; }
        public string? Created_by { get; set; }
        public string? Modified_by { get; set; }
        public string? Status { get; set; }
    }
}
