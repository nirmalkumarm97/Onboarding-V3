using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Diagnostics;

namespace EmployeeOnboarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _Ilogin;
        public LoginController(ILogin Ilogin)
        {        
          _Ilogin = Ilogin;
        }

        [HttpPost("LoginInvite")]
        public async Task<IActionResult> LoginInvite(List<logininviteVM>logindet)
        {
            try
            {

                var response = await _Ilogin.LoginInvite(logindet);
                if (response != null)
                {
                    return Ok(response);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

        [HttpPut("LoginCmp")]
        public async Task<IActionResult> LoginCmp(string Emailid, loginconfirmVM logindet)
        {
            var response = await _Ilogin.LoginCmp(Emailid,logindet);
            if (response != null)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPost("AuthenticateEmp")]
        public async Task<IActionResult> AuthenticateEmp(string emailid, string password)
        {
            try 
            {
                var response = await _Ilogin.AuthenticateEmp(emailid, password);
                if (response != null)
                {
                    return Ok(response);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string emailId)
        {
            try
            {
                var response = await _Ilogin.ForgotPassword(emailId);
                if (response != null)
                {
                    return Ok(response);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(string emailId, loginconfirmVM loginconfirmVM)
        {
            try
            {
                var response = await _Ilogin.UpdatePassword(emailId, loginconfirmVM);
                if (response != null)
                {
                    return Ok(response);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }
        [HttpPost("VerifyOTP")]

        public async Task<IActionResult> VerifyOTP(string emailId, int OTP)
        {
            try
            {

                var response = await _Ilogin.VerifyOTP(emailId, OTP);
                if (response != null)
                {
                    return Ok(response);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

    }
}
