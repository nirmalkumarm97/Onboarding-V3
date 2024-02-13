using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Models;


namespace EmployeeOnboarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAdminRepository _adminRepository;
        public AdminController(ApplicationDbContext context, IAdminRepository adminRepository)
        {
            _context = context;
            _adminRepository = adminRepository;
        }

        [HttpGet("api/AdminDashboard")]
        public async Task<List<DashboardVM>> getEmployee()
        {
            return await _adminRepository.GetEmployeeDetails();
        }

        ////[HttpPost("api/AdminDeleteById")]
        ////public async Task deleteEmployee(string[] employeeid)
        ////{
        ////    await _adminRepository.DeleteEmployee(employeeid);
        ////}   

        //[HttpGet("api/GetEmployeeDetails")]
        //public async Task<List<PersonalInfoVM>> GetPersonalInfo(int employee)
        //{
        //    return await _adminRepository.GetPersonalInfo(employee);
        //}

        //[HttpGet("api/GetApprovedEmployeeDetails")]
        //public async Task<List<ApprovedUserDetails>> GetApprovedEmployeeDetails(int EmpGen_Id)
        //{
        //    return await _adminRepository.GetApprovedEmpDetails(EmpGen_Id);
        //}

        [HttpGet("api/GetPendingEmployeeDetails")]
        public async Task<List<Dashboard1VM>> GetPendingEmployee()
        {
            return await _adminRepository.GetPendingEmployeeDetails();
        }
        [HttpGet("api/GetInvitedEmployeeDetails")]
        public async Task<List<Dashboard1VM>> GetInvitedEmployee()
        {
            return await _adminRepository.GetInvitedEmployeeDetails();
        }
        [HttpGet("api/GetRejectedEmployeeDetails")]
        public async Task<List<Dashboard1VM>> GetRejectedDetails()
        {
            return await _adminRepository.GetRejectedEmployeeDetails();
        }

        //[HttpPost("api/SearchApprovedEmployeeDetails")]
        //public async Task<List<DashboardVM>> SearchApprovedEmployee(string name)
        //{
        //    return await _adminRepository.SearchApprovedEmpDetails(name);
        //}

        //[HttpPost("api/SearchPendingEmployeeDetails")]
        //public async Task<List<Dashboard1VM>> SearchPendingEmployee(string name)
        //{
        //    return await _adminRepository.SearchPendingEmpDetails(name);
        //}

        //[HttpPost("api/SearchInvitedEmployeeDetails")]
        //public async Task<List<Dashboard1VM>> SearchInvitedEMployee(string name)
        //{
        //    return await _adminRepository.SearchInvitedEmpDetails(name);
        //}

        //[HttpPost("api/SearchRejectedEmployeeDetails")]
        //public async Task<List<Dashboard1VM>> SearchRejectedEmployeeDetails(string name)
        //{
        //    return await _adminRepository.SearchRejectedEmpDetails(name);
        //}
    }
}
