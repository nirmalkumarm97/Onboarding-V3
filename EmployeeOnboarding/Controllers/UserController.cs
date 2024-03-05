using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Repository;
using EmployeeOnboarding.Services;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnboardingWebsite.Models;
using System.ComponentModel.Design;

namespace EmployeeOnboarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("add-education/{genId}")]
        public async Task<IActionResult> AddEducation(int genId, [FromBody] List<EducationVM> educations)
        {
            var response = await _userRepository.AddEducation(genId , educations);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }


        [HttpPost("add-certificate/{genId}")]
        public async Task<IActionResult> AddCertificate(int genId, [FromBody] List<CertificateVM> certificates)
        {
            var response = await _userRepository.AddCertificate(genId, certificates);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("add-experience/{genId}")]
        public async Task<IActionResult> AddExperience(int genId, [FromBody] List<WorkExperienceVM> experiences)
        {
            var response = await _userRepository.AddExperiences(genId, experiences);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("add-reference/{genId}")]
        public async Task<IActionResult> AddReference(int genId, [FromBody] ReferenceVM reference)
        {
            var response = await _userRepository.AddReference(genId, reference);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("add-health/{genId}")]
        public async Task<IActionResult> AddHealth(int genId, [FromBody] HealthVM health)
        {
            var response = await _userRepository.AddHealth(genId, health);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("add-existing-bank/{genId}")]
        public async Task<IActionResult> AddBank(int genId, [FromBody] ExistingBankVM health)
        {
            var response = await _userRepository.AddBank(genId, health);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
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