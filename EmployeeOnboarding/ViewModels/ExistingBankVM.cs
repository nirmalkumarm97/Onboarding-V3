namespace EmployeeOnboarding.ViewModels
{
    public class ExistingBankVM
    {
        public string? Account_name { get; set; }
        public string? Bank_name { get; set; }
        public string? Bank_Branch { get; set; }
        public long? Account_number { get; set; }
        public string? IFSC_code { get; set; }
        public bool? Joint_Account { get; set; }
        public List<string> ProofSubmitted { get; set; }
        public IFormFile? Bank_Documents { get; set; }
    }
}
