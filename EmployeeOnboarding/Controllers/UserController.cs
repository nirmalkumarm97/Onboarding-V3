using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Repository;
using EmployeeOnboarding.Request;
using EmployeeOnboarding.Response;
using EmployeeOnboarding.Services;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnboardingWebsite.Models;
using System;
using System.ComponentModel.Design;

namespace EmployeeOnboarding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;

        public UserController(IUserRepository userRepository, ApplicationDbContext applicationDbContext)
        {
            _userRepository = userRepository;
            _context = applicationDbContext;
        }

        [HttpPost("add-education/{genId}")]
        public async Task<IActionResult> AddEducation(int genId, [FromBody] List<EducationVM> educations)
        {
            try
            {
                var response = await _userRepository.AddEducation(genId, educations);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }


        [HttpPost("add-certificate/{genId}")]
        public async Task<IActionResult> AddCertificate(int genId, [FromBody] List<CertificateVM> certificates)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }

        }

        [HttpPost("add-experience/{genId}")]
        public async Task<IActionResult> AddExperience(int genId, [FromBody] List<WorkExperienceVM> experiences)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

        [HttpPost("add-reference/{genId}")]
        public async Task<IActionResult> AddReference(int genId, [FromBody] ReferenceVM reference)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

        [HttpPost("add-health/{genId}")]
        public async Task<IActionResult> AddHealth(int genId, [FromBody] HealthVM health)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

        [HttpPost("add-existing-bank/{genId}")]
        public async Task<IActionResult> AddBank(int genId, [FromBody] ExistingBankVM bank)
        {
            try
            {
                var response = await _userRepository.AddBank(genId, bank);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

        [HttpPost("CreateSelfDeclaration/{genId}")]
        public async Task<IActionResult> CreateSelfDeclaration(int genId, [FromBody] SelfDeclarationRequest selfDeclarationRequest)
        {
            try
            {
                var name = _context.EmployeeGeneralDetails.Where(x => x.Id == genId && x.Empname.Contains(selfDeclarationRequest.Name)).Select(x => x.Empname).FirstOrDefault();
                if (name != null)
                {
                    var response = await _userRepository.CreateSelfDeclaration(genId, selfDeclarationRequest);
                    if (response != null)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else return BadRequest("Name should be same as like PersonalInformation");
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

        //*************************************************************************************************************

        [HttpGet("get-education/{genId}")]
        public IActionResult GetEducation(int genId)
        {
            try
            {
                var education = _userRepository.GetEducation(genId);
                return Ok(education);
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }


        [HttpGet("get-certificate/{genId}")]
        public IActionResult GetCertificate(int genId)
        {
            try
            {
                var certificate = _userRepository.GetCertificate(genId);
                return Ok(certificate);
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }


        [HttpGet("get-experience/{genId}")]
        public IActionResult GetCompanyExperiences(int genId)
        {
            try
            {
                var companyExperiences = _userRepository.GetCompanyByEmpId(genId);
                return Ok(companyExperiences);
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

    [HttpGet("get-reference/{genId}")]
        public IActionResult GetReference(int genId)
        {
            try
            {
                var reference = _userRepository.Getreference(genId);
                return Ok(reference);
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

        [HttpGet("get-health/{genId}")]
        public IActionResult GetHealth(int genId)
        {
            try
            {
                var healthInfo = _userRepository.GetHealth(genId);
                return Ok(healthInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

        [HttpGet("get-existing-bank/{genId}")]
        public IActionResult GetBank(int genId)
        {
            try
            {
                var bank = _userRepository.GetBank(genId);
                return Ok(bank);
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCode(400, ex.Message));
            }
        }

        [HttpGet("GetSelfDeclaration/{genId}")]
        public async Task<IActionResult> GetSelfDeclaration(int genId)
        {
            try
            {
                var response = await _userRepository.GetSelfDeclaration(genId);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception ex)
            { 
                return BadRequest(ex.Message); 
            }
        }
    }
}