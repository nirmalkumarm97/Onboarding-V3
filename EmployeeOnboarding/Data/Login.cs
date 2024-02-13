using System.ComponentModel.DataAnnotations;

namespace EmployeeOnboarding.Data
{
    public class Login 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string? Password { get; set; }
        public string? Invited_Status { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Modified { get; set; }
        public string Created_by { get; set; }
        public string? Modified_by { get; set; }
        public string Status { get; set; }

    }
}
