using EmployeeOnboarding.Data;
using EmployeeOnboarding.ViewModels;

namespace EmployeeOnboarding.Contracts
{
    public interface ILogin
    {
        Task<IEnumerable<Login>> getemp();
        Task<employloginVM> AuthenticateEmp(string emailid, string password);
        int LoginInvite(logininviteVM logindet);
        Task<Login> LoginCmp(string Emailid, loginconfirmVM logindet);
        Task<int> ForgotPassword(string emailId);
        Task<string> UpdatePassword(string emailId, loginconfirmVM loginconfirmVM);
    }
}
