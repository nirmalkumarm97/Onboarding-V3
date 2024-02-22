namespace EmployeeOnboarding.Request
{
    public class RequiredInfo
    {
        public byte[] Aadhar { get; set; }
        public byte[] Pan { get; set; }
        public byte[]? Driving_license { get; set; }
        public byte[]? Passport { get; set; }

    }
}
