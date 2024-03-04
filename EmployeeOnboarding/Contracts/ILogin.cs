using EmployeeOnboarding.Data;
using EmployeeOnboarding.ViewModels;

namespace EmployeeOnboarding.Contracts
{
    public interface ILogin
    {
        Task<IEnumerable<Login>> getemp();
        Task<employloginVM> AuthenticateEmp(string emailid, string password);
        Task<string> LoginInvite(List<logininviteVM> logindet);
        Task<Login> LoginCmp(string Emailid, loginconfirmVM logindet);
        Task<bool> ForgotPassword(string emailId);
        Task<string> UpdatePassword(string emailId, loginconfirmVM loginconfirmVM);
        Task<bool> VerifyOTP(string emailId, int OTP);
    }
}
