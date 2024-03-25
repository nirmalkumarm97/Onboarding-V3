namespace EmployeeOnboarding.Data
{
    public class SelfDeclaration
    {
        public int Id { get; set; }
        public int EmpGen_Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}