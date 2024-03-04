using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnboardingWebsite.Models;

namespace EmployeeOnboarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("add-education/{genId}")]
        public async Task<IActionResult> AddEducation(int genId, [FromBody] List<EducationVM> educations)
        {
            var data = _userRepository.AddEducation(genId, educations);
            return Ok(data + "Sucess");
        }


        [HttpPost("add-certificate/{genId}")]
        public async Task<IActionResult> AddCertificate(int genId, [FromBody] List<CertificateVM> certificates)
        {
            var data = _userRepository.AddCertificate(genId, certificates);
            return Ok(data + "Sucess");
        }

        [HttpPost("add-experience/{genId}")]
        public async Task<IActionResult> AddExperience(int genId, [FromBody] List<WorkExperienceVM> experiences)
        {
            var data = _userRepository.AddExperiences(genId, experiences);
            return Ok(data + "Sucess");
        }

        [HttpPost("add-reference/{genId}")]
        public IActionResult AddReference(int genId, [FromBody] ReferenceVM reference)
        {
            _userRepository.AddReference(genId, reference);
            return Ok();
        }

        [HttpPost("add-health/{genId}")]
        public IActionResult AddHealth(int genId, [FromBody] HealthVM health)
        {
            _userRepository.AddHealth(genId, health);
            return Ok();
        }

        [HttpPost("add-existing-bank/{genId}")]
        public IActionResult AddBank(int genId, [FromBody] ExistingBankVM health)
        {
            _userRepository.AddBank(genId, health);
            return Ok();
        }



        //*************************************************************************************************************

        [HttpGet("get-education/{genId}")]
        public IActionResult GetEducation(int genId)
        {
            var education = _userRepository.GetEducation(genId);
            return Ok(education);
        }


        [HttpGet("get-certificate/{genId}")]
        public IActionResult GetCertificate(int genId)
        {
            var certificate = _userRepository.GetCertificate(genId);
            return Ok(certificate);
        }


        [HttpGet("get-experience/{genId}")]
        public IActionResult GetCompanyExperiences(int genId)
        {
            var companyExperiences = _userRepository.GetCompanyByEmpId(genId);
            return Ok(companyExperiences);
        }

        [HttpGet("get-reference/{genId}")]
        public IActionResult GetReference(int genId)
        {
            var reference = _userRepository.Getreference(genId);
            return Ok(reference);
        }

        [HttpGet("get-health/{genId}")]
        public IActionResult GetHealth(int genId)
        {
            var healthInfo = _userRepository.GetHealth(genId);
            return Ok(healthInfo);
        }

        [HttpGet("get-existing-bank/{genId}")]
        public IActionResult GetBank(int genId)
        {
            var bank = _userRepository.GetBank(genId);
            return Ok(bank);
        }

    }
}