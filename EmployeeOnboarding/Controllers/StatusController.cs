using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Repository;
using EmployeeOnboarding.ViewModels;
using EmployeeOnboarding.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeOnboarding.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class StatusController : ControllerBase
    {

        public StatusRepository _onboardstatusService;
        public ApplicationDbContext _context;

        public StatusController(StatusRepository onboardstatusService, ApplicationDbContext context)
        {
            _onboardstatusService = onboardstatusService;
            _context = context;
        }

        [HttpPost("approve/{genId}")]
        public async Task<IActionResult> ChangeApprovalStatus(int genId, [FromBody] onboardstatusVM onboardstatus)
        {
            if (onboardstatus.Emp_id.IsNullOrEmpty() || onboardstatus.Official_EmailId.IsNullOrEmpty())
            {
                return BadRequest("Please fill in all the mandatory fields");
            }
            var empCheck = _context.EmployeeGeneralDetails.ToList();

            if (empCheck != null && empCheck.Any())
            {
                var existingEmployee = empCheck.FirstOrDefault(e => e.Empid == onboardstatus.Emp_id);
                var existingEmail = empCheck.FirstOrDefault(e => e.Official_EmailId == onboardstatus.Official_EmailId);
                var both = empCheck.FirstOrDefault(e => e.Official_EmailId == onboardstatus.Official_EmailId && e.Empid == onboardstatus.Emp_id);

                if (existingEmployee != null)
                {
                    return BadRequest("Employee ID already exists.");
                }
                if (existingEmail != null)
                {
                    return BadRequest("Email ID already exists.");
                }
                if (both != null)
                {
                    return BadRequest("Employee ID and Email ID both already exist.");
                }
            }
            try
            {
                await _onboardstatusService.ChangeApprovalStatus(genId, onboardstatus);
                return Ok("Approved");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("reject/{genId}")]
        public async Task<IActionResult> ChangeCancelStatus(int genId, [FromBody] commentVM onboardstatus)
        {
            try
            {
                await _onboardstatusService.ChangeCancelStatus(genId, onboardstatus);
                return Ok("Rejected");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpGet("GetStagesbyGenId/{genId}")]
        public async Task<IActionResult> GetStagesbyGenId(int genId)
        {
            var issuccess = await _onboardstatusService.GetStagesbyGenId(genId);

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
