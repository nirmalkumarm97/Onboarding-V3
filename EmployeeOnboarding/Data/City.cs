using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Data
{
    public class City
    {
        public int Id { get; set; }
        [ForeignKey("State_Id")]
     public int State_Id { get; set; }
        public string City_Name { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Modified { get; set; }
    }
}
