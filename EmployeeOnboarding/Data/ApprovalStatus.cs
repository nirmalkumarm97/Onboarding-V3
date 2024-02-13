using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Data
{
    public class ApprovalStatus
    {
        public int Id { get; set; }

        [ForeignKey("EmpGen_Id")]
        public int EmpGen_Id { get; set; }
        public int Current_Status { get; set; }
        public string? Comments { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Modified { get; set; }
        public string Created_by { get; set; }
        public string? Modified_by { get; set; }
        public string Status { get; set; }
        //[ForeignKey("Login_Id")]
        // public int Login_Id { get;  set; }
    }
}
