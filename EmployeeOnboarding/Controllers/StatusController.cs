using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Repository;
using EmployeeOnboarding.ViewModels;
using EmployeeOnboarding.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeOnboarding.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class StatusController : ControllerBase
    {

        public OnboardStatusRepository _onboardstatusService;
        public ApplicationDbContext _context;

        public StatusController(OnboardStatusRepository onboardstatusService, ApplicationDbContext context)
        {
            _onboardstatusService = onboardstatusService;
            _context = context;
        }

        [HttpPost("approve/{genId}")]
        public async Task<IActionResult> ChangeApprovalStatus(int genId, [FromBody] onboardstatusVM onboardstatus)
        {
            await _onboardstatusService.ChangeApprovalStatus(genId, onboardstatus);
            return Ok("Approved");
        }
         
        [HttpPost("reject/{genId}")]
        public async Task<IActionResult> ChangeCancelStatus(int genId, [FromBody] commentVM onboardstatus)
        {
           await _onboardstatusService.ChangeCancelStatus(genId, onboardstatus);
            return Ok("Rejected");
        }

        [HttpPost("pending/{genId}")]
        public IActionResult ChangePendingStatus(int genId)
        {
            _onboardstatusService.ChangePendingStatus(genId);
            return Ok("Pending");
        }

        [HttpGet("reject-comment/{genId}")]
        public IActionResult RejectedComment(int genId)
        {
            var issuccess = _onboardstatusService.RejectedComment(genId);

            return Ok(issuccess);
        }

        //[HttpGet("status-dashboard")]
        //public async Task<statusdashVM> GetAdminStatusList()
        //{
        //    var upstatus = await _context.Approvals.ToListAsync();
        //    var model = new statusdashVM
        //    {
        //        TotalRequests = upstatus.Count,
        //        ApprovedRequests = upstatus.Count(q => q.Approved == true),
        //        PendingRequests = leaveRequests.Count(q => q.Cancelled == null),
        //        RejectedRequests = upstatus.Count(q => q.Cancelled == true),
        //    };

        //    return model;
        //}
    }

}
