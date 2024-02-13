namespace EmployeeOnboarding.Models
{
    public class EducationDetailsVM
    {
        public string programme { get; set; }
        public string CollegeName { get; set; }
        public string Degree { get; set; }
        public string Major { get; set; }
        public int PassedoutYear { get; set; }
        public byte[] Certificate { get; set; }
        
    }
}
