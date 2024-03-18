namespace EmployeeOnboarding.Models
{
    public class Dashboard1VM
    {
        public int Login_Id { get; set; }
        public int? EmpGen_Id { get; set; }
        public double Contact_no { get; set; }
        public string Name { get; set; }
        public DateTime? DateModified { get; set; }
        public string Email_id { get; set; }
        public string Current_Status { get; set; }
        public int? UserId { get; set; }
        public string? RejectedComments { get; set; }
    }
}
