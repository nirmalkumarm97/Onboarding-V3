using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Request;

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

        [HttpPost("api/AdminDashboard")]
        public async Task<IActionResult> getEmployee([FromBody] AdminRequest adminRequest)
        {
            var result = await _adminRepository.GetEmployeeDetails(adminRequest);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("api/GetExpiredDetails")]
        public async Task<IActionResult> GetExpiredDetails(AdminRequest adminRequest)
        {
            var result = await _adminRepository.GetExpiredDetails(adminRequest);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
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

        [HttpPost("api/GetPendingEmployeeDetails")]
        public async Task<IActionResult> GetPendingEmployeeDetails([FromBody] AdminRequest adminRequest)
        {
            var result = await _adminRepository.GetPendingEmployeeDetails(adminRequest);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost("api/GetInvitedEmployeeDetails")]
        public async Task<IActionResult> GetInvitedEmployee([FromBody] AdminRequest adminRequest)
        {
            var result = await _adminRepository.GetInvitedEmployeeDetails(adminRequest);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost("api/GetRejectedEmployeeDetails")]
        public async Task<IActionResult> GetRejectedEmployeeDetails([FromBody] AdminRequest adminRequest)
        {
            var result = await _adminRepository.GetRejectedEmployeeDetails(adminRequest);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
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
