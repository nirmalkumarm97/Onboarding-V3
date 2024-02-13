using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Data
{
    public class EmployeeGeneralDetails
    {
        public int Id { get; set; }
        [ForeignKey("Login_Id")]
        public int Login_ID { get; set; }    
        public string? Empid { get; set; }
        public string Empname { get; set; }
        public string? Official_EmailId { get; set; }
        public string Personal_Emailid { get; set; }
        public long Contact_no { get; set; }
        public DateOnly DOB {  get; set; }
        public string Nationality { get; set; }
        public int Gender { get; set; }
        public int? MaritalStatus { get; set; }
        public DateOnly? DateOfMarriage { get; set; }
        public int BloodGrp { get; set; }
        public string Profile_pic { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime? Date_Modified { get; set; }
        public string Created_by { get; set; }
        public string? Modified_by { get; set; }
        public string Status { get; set; }

    }
}
