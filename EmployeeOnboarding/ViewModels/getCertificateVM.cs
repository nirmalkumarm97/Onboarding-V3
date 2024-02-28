namespace EmployeeOnboarding.ViewModels
{
    public class getCertificateVM
    {
        public int? GenId { get; set; }
        public string? Certificate_name { get; set; }
        public string? Issued_by { get; set; }
        public DateOnly? Valid_till { get; set; }
        public int? Duration { get; set; }
        public string? Percentage { get; set; }
        public byte[]? proof { get; set; }


    }
}
