using EmployeeOnboarding.Data;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.Request;
using EmployeeOnboarding.Response;
//using EmployeeOnboarding.Models;

namespace EmployeeOnboarding.Contracts
{
    public interface IAdminRepository
    {
        Task <List<Dashboard1VM>> GetPendingEmployeeDetails(AdminRequest adminRequest);
        Task<List<Dashboard1VM>> GetInvitedEmployeeDetails(AdminRequest adminRequest);
        Task<List<Dashboard1VM>> GetRejectedEmployeeDetails(AdminRequest adminRequest);
        Task<List<DashboardVM>> GetEmployeeDetails(AdminRequest adminRequest);
        Task<List<Dashboard1VM>> GetExpiredDetails(AdminRequest adminRequest);

        //// Task DeleteEmployee(string[] employeeId);    
        //Task <List<PersonalInfoVM>>? GetPersonalInfo(int id);
        //Task<List<ApprovedUserDetails>>? GetApprovedEmpDetails(int id);
        //Task<List<DashboardVM>>? SearchApprovedEmpDetails(string name);
        //Task<List<Dashboard1VM>>? SearchPendingEmpDetails(string name);
        //Task<List<Dashboard1VM>>? SearchInvitedEmpDetails(string name);
        //Task<List<Dashboard1VM>>? SearchRejectedEmpDetails(string name);
    }
}
