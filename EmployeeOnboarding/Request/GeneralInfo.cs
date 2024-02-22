namespace EmployeeOnboarding.Request
{
    public class GeneralInfo
    {
        public string Empname { get; set; }
        public string Personal_Emailid { get; set; }
        public long Contact_no { get; set; }
        public string DOB { get; set; }
        public string Nationality { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public string? DateOfMarriage { get; set; }
        public int BloodGrp { get; set; }
        public byte[] Profile_pic { get; set; }

    }
}
