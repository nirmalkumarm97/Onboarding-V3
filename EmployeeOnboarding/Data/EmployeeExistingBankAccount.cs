using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Data
{
    public class EmployeeExistingBankAccount

    {
        public int Id { get; set; }
        [ForeignKey("EmpGen_Id")]
        public int? EmpGen_Id { get; set; }  
        public string? Account_name { get; set; }
        public string? Bank_name { get; set; }
        public string? Bank_Branch { get; set; }
        public long? Account_number { get; set; }
        public string? IFSC_code { get; set; }
        public bool? Joint_Account { get; set; }
        public string? Proof_submitted { get; set; }
        public string? Bank_Documents { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Modified { get; set; }
        public string? Created_by { get; set; }
        public string? Modified_by { get; set; }
        public string? Status { get; set; }
    }
}
