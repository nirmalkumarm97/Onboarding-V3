using EmployeeOnboarding.Data;
using EmployeeOnboarding.Models;
//using EmployeeOnboarding.Models;

namespace EmployeeOnboarding.Contracts
{
    public interface IAdminRepository
    {
        Task <List<Dashboard1VM>> GetPendingEmployeeDetails();
        Task<List<Dashboard1VM>> GetInvitedEmployeeDetails();
        Task<List<Dashboard1VM>> GetRejectedEmployeeDetails();
        Task<List<DashboardVM>> GetEmployeeDetails();
        //// Task DeleteEmployee(string[] employeeId);    
        //Task <List<PersonalInfoVM>>? GetPersonalInfo(int id);
        //Task<List<ApprovedUserDetails>>? GetApprovedEmpDetails(int id);
        //Task<List<DashboardVM>>? SearchApprovedEmpDetails(string name);
        //Task<List<Dashboard1VM>>? SearchPendingEmpDetails(string name);
        //Task<List<Dashboard1VM>>? SearchInvitedEmpDetails(string name);
        //Task<List<Dashboard1VM>>? SearchRejectedEmpDetails(string name);

    }
}
