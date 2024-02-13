using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
//using EmployeeOnboarding.Data.Services;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.ViewModels;
using OnboardingWebsite.Models;
using EmployeeOnboarding.Services;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace EmployeeOnboarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        //private readonly EducationService _educationService;
        private readonly WorkExperienceService _experienceService; // Corrected the type here.
        private readonly ReferenceService _referenceService;
        private readonly EducationService _educationService;
        private readonly CertificateService _certificateService;
        private readonly ExistingBankService _existingBankservice;
        private readonly HealthService _healthService;

        public UserController(WorkExperienceService experienceService, ReferenceService referenceService, EducationService educationService,CertificateService certificateService,HealthService healthService,ExistingBankService existingBankService)
        {
            _educationService = educationService;
            _experienceService = experienceService;
            _referenceService = referenceService;
            _certificateService = certificateService;
            _healthService = healthService;
            _existingBankservice = existingBankService;
        }


        [HttpPost("add-education/{empId}")]
        public async Task<IActionResult> AddEducation(int empId, [FromBody] List<EducationVM> educations)
        {
            var data = _educationService.AddEducation(empId, educations);
            return Ok(data + "Sucess");
        }


        [HttpPost("add-certificate/{empId}")]
        public async Task<IActionResult> AddCertificate(int empId, [FromBody] List<CertificateVM> certificates)
        {
            var data = _certificateService.AddCertificate(empId, certificates);
            return Ok(data + "Sucess");
        }

        [HttpPost("add-experience/{empId}")]
        public async Task<IActionResult> AddExperience(int empId, [FromBody] List<WorkExperienceVM> experiences)
        {
           var data = _experienceService.AddExperiences(empId, experiences);
            return Ok(data + "Sucess");
        }

        [HttpPost("add-reference/{empId}")]
        public IActionResult AddReference(int empId, [FromBody] ReferenceVM reference)
        {
            _referenceService.AddReference(empId, reference);
            return Ok();
        }

        [HttpPost("add-health/{empId}")]
        public IActionResult AddHealth(int empId, [FromForm] HealthVM health)
        {
            _healthService.AddHealth(empId, health);
            return Ok();
        }

        [HttpPost("add-existing-bank/{empId}")]
        public IActionResult AddBank(int empId, [FromForm] ExistingBankVM health)
        {
            _existingBankservice.AddBank(empId, health);
            return Ok();
        }



        //*************************************************************************************************************

        [HttpGet("get-education/{empId}")]
        public IActionResult GetEducation(int empId)
        {
            var education = _educationService.GetEducation(empId);
            return Ok(education);
        }


        [HttpGet("get-certificate/{empId}")]
        public IActionResult GetCertificate(int empId)
        {
            var certificate = _certificateService.GetCertificate(empId);
            return Ok(certificate);
        }


        [HttpGet("get-experience/{empId}")]
        public IActionResult GetCompanyExperiences(int empId)
        {
            var companyExperiences = _experienceService.GetCompanyByEmpId(empId);
            return Ok(companyExperiences);
        }

        [HttpGet("get-reference/{empId}")]
        public IActionResult GetReference(int empId)
        {
            var reference = _referenceService.Getreference(empId);
            return Ok(reference);
        }

        [HttpGet("get-health/{empId}")]
        public IActionResult GetHealth(int empId)
        {
            var healthInfo = _healthService.GetHealth(empId);
            return Ok(healthInfo);
        }

        [HttpGet("get-existing-bank/{empId}")]
        public IActionResult GetBank(int empId)
        {
            var bank = _existingBankservice.GetBank(empId);
            return Ok(bank);
        }

    }
}
