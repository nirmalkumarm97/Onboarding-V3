using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeOnboarding.ViewModels
{
    public class ContactVM
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [ForeignKey("Country_Id")]
        public int Country_Id { get; set; }
        [ForeignKey("State_Id")]
        public int State_Id { get; set; }
        [ForeignKey("City_Id")]
        public int City_Id { get; set; }
        public string Pincode { get; set; }
    }
}
