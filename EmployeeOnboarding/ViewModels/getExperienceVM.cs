namespace EmployeeOnboarding.ViewModels
{
    public class getExperienceVM
    {
        public int? GenId { get; set; }
        public int Number { get; set; }
        public string? Company_name { get; set; }
        public string? Designation { get; set; }
        public string? Reason { get; set; }
        public string? Location { get; set;}
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Reporting_to { get; set; }
        public byte[]? Exp_Certificate { get; set; }
        public string? Exp_CertificateName { get; set; }
        //public byte[]? getCertificate { get; set; }
    }
}
