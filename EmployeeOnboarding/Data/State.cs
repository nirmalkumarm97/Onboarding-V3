using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.Data
{
    public class State
    {
        public int Id { get; set; }
        [ForeignKey("Country_Id")]
        public int Country_Id { get; set; }
        public string State_Name { get; set; }
        public DateTime Date_Created { get; set; }
        public DateTime Date_Modified{ get; set;}
    }
}
