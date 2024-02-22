using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Request;
using EmployeeOnboarding.Response;
using EmployeeOnboarding.Services;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnboardingWebsite.Models;
//using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUserDetailsRepository _userDetailsRepository;

        public UserDetailsController(IUserDetailsRepository userDetailsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
        }


        [HttpPost("AddPersonalInfo")]
        public async Task<IActionResult> AddPersonalInfo([FromForm] PersonalInfoRequest personalInfoRequest)
        {
            var response = _userDetailsRepository.AddPersonalInfo(personalInfoRequest);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetPersonalInfo")]
        public async Task<IActionResult> GetPersonalInfo(int loginId)
        {
            var response = _userDetailsRepository.GetPersonalInfo(loginId);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
       

        

       