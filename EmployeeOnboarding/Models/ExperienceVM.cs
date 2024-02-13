namespace EmployeeOnboarding.Models
{
    public class ExperienceVM
    {
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string ReasonForLeaving { get; set; }
        public int TotalNoofMonths { get; set; }
        public byte[]? ExperienceCerti { get; set; }
    }
}
