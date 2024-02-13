using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Repository;
using EmployeeOnboarding.Services;
using EmployeeOnboarding.ViewModels;
using EmployeeOnboarding.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeOnboarding.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class StatusController : ControllerBase
    {

        public onboardstatusService _onboardstatusService;
        public ApplicationDbContext _context;

        public StatusController(onboardstatusService onboardstatusService, ApplicationDbContext context)
        {
            _onboardstatusService = onboardstatusService;
            _context = context;
        }

        [HttpPost("approve/{eid}")]
        public IActionResult ChangeApprovalStatus(int eid,[FromBody] onboardstatusVM onboardstatus)
        {
            _onboardstatusService.ChangeApprovalStatus(eid,onboardstatus);
            return Ok("Approved");
        }

        [HttpPost("reject/{eid}")]
        public IActionResult ChangeCancelStatus(int eid, [FromBody] commentVM onboardstatus)
        {
            _onboardstatusService.ChangeCancelStatus(eid, onboardstatus);
            return Ok("Rejected");
        }

        [HttpPost("pending/{eid}")]
        public IActionResult ChangePendingStatus(int eid)
        {
            _onboardstatusService.ChangePendingStatus(eid);
            return Ok("Pending");
        }

        [HttpGet("reject-comment/{id}")]
        public IActionResult RejectedComment(int id)
        {
            var issuccess = _onboardstatusService.RejectedComment(id);

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
