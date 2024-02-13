using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Data
{
    public class EmployeeReferenceDetails
    {
        public int Id { get; set; }
        [ForeignKey("EmpGen_Id")]
        public int? EmpGen_Id { get; set; }
        public string? Referral_name { get; set; }
        public string? Designation { get; set; }
        public string? Company_name { get; set; }
        public long? Contact_number { get; set; }
        public string? Email_Id { get; set; }
        public bool? Authorize { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Modified { get; set; }
        public string? Created_by { get; set; }
        public string? Modified_by { get; set; }
        public string? Status { get; set; }

    }
}
