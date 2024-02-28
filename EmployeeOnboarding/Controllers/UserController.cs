using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
//using EmployeeOnboarding.Data.Services;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.ViewModels;
using OnboardingWebsite.Models;
using EmployeeOnboarding.Services;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;

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


        [HttpPost("add-education/{genId}")]
        public async Task<IActionResult> AddEducation(int genId, [FromBody] List<EducationVM> educations)
        {
            var data = _educationService.AddEducation(genId, educations);
            return Ok(data + "Sucess");
        }


        [HttpPost("add-certificate/{genId}")]
        public async Task<IActionResult> AddCertificate(int genId, [FromBody] List<CertificateVM> certificates)
        {
            var data = _certificateService.AddCertificate(genId, certificates);
            return Ok(data + "Sucess");
        }

        [HttpPost("add-experience/{genId}")]
        public async Task<IActionResult> AddExperience(int genId, [FromBody] List<WorkExperienceVM> experiences)
        {
           var data = _experienceService.AddExperiences(genId, experiences);
            return Ok(data + "Sucess");
        }

        [HttpPost("add-reference/{genId}")]
        public IActionResult AddReference(int genId, [FromBody] ReferenceVM reference)
        {
            _referenceService.AddReference(genId, reference);
            return Ok();
        }

        [HttpPost("add-health/{genId}")]
        public IActionResult AddHealth(int genId, [FromForm] HealthVM health)
        {
            _healthService.AddHealth(genId, health);
            return Ok();
        }

        [HttpPost("add-existing-bank/{genId}")]
        public IActionResult AddBank(int genId, [FromForm] ExistingBankVM health)
        {
            _existingBankservice.AddBank(genId, health);
            return Ok();
        }



        //*************************************************************************************************************

        [HttpGet("get-education/{genId}")]
        public IActionResult GetEducation(int genId)
        {
            var education = _educationService.GetEducation(genId);
            return Ok(education);
        }


        [HttpGet("get-certificate/{genId}")]
        public IActionResult GetCertificate(int genId)
        {
            var certificate = _certificateService.GetCertificate(genId);
            return Ok(certificate);
        }


        [HttpGet("get-experience/{genId}")]
        public IActionResult GetCompanyExperiences(int genId)
        {
            var companyExperiences = _experienceService.GetCompanyByEmpId(genId);
            return Ok(companyExperiences);
        }

        [HttpGet("get-reference/{genId}")]
        public IActionResult GetReference(int genId)
        {
            var reference = _referenceService.Getreference(genId);
            return Ok(reference);
        }

        [HttpGet("get-health/{genId}")]
        public IActionResult GetHealth(int genId)
        {
            var healthInfo = _healthService.GetHealth(genId);
            return Ok(healthInfo);
        }

        [HttpGet("get-existing-bank/{genId}")]
        public IActionResult GetBank(int genId)
        {
            var bank = _existingBankservice.GetBank(genId);
            return Ok(bank);
        }

    }
}
