using EmployeeOnboarding.Data;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.Request;
using EmployeeOnboarding.Response;
//using EmployeeOnboarding.Models;

namespace EmployeeOnboarding.Contracts
{
    public interface IAdminRepository
    {
        Task <AdminDashBoardUsersData> GetPendingEmployeeDetails(AdminRequest adminRequest);
        Task<AdminDashBoardUsersData> GetInvitedEmployeeDetails(AdminRequest adminRequest);
        Task<AdminDashBoardUsersData> GetRejectedEmployeeDetails(AdminRequest adminRequest);
        Task<AdminDashBoardEmployeesData> GetEmployeeDetails(AdminRequest adminRequest);
        Task<AdminDashBoardUsersData> GetExpiredDetails(AdminRequest adminRequest);

        //// Task DeleteEmployee(string[] employeeId);    
        //Task <List<PersonalInfoVM>>? GetPersonalInfo(int id);
        //Task<List<ApprovedUserDetails>>? GetApprovedEmpDetails(int id);
        //Task<List<DashboardVM>>? SearchApprovedEmpDetails(string name);
        //Task<List<Dashboard1VM>>? SearchPendingEmpDetails(string name);
        //Task<List<Dashboard1VM>>? SearchInvitedEmpDetails(string name);
        //Task<List<Dashboard1VM>>? SearchRejectedEmpDetails(string name);
    }
}
