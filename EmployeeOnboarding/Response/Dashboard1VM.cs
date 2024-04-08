namespace EmployeeOnboarding.Response
{
    public class Dashboard1VM
    {
        public int Login_Id { get; set; }
        public int? EmpGen_Id { get; set; }
        public double Contact_no { get; set; }
        public string Name { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email_id { get; set; }
        public string Current_Status { get; set; }
        public string? RejectedComments { get; set; }
    }
    public class AdminDashBoardUsersData
    {
        public int OverallCount { get; set; }
        public List<Dashboard1VM> result { get; set; }
    }
}
