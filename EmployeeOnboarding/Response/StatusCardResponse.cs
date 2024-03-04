namespace EmployeeOnboarding.Response
{
    public class StatusCardResponse
    {
        public int LoginId { get; set; }
        public int? UserId { get; set; }
        public int? GenId { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
    }
}
