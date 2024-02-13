using EmployeeOnboarding.Data;
using EmployeeOnboarding.ViewModels;

namespace EmployeeOnboarding.Contracts
{
    public interface ILogin
    {
        Task<IEnumerable<Login>> getemp();
        Task<employloginVM> AuthenticateEmp(string emailid, string password);
    }
}
