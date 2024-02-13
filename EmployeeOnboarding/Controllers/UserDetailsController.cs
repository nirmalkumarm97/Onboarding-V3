using EmployeeOnboarding.Data;
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
        private readonly GeneralDetailService _generalservices;
        private readonly FamilyService _familyService;
        private readonly HobbyMembershipService _hobbyService;
        private readonly RequiredService _requiredService;
        private readonly EmergencyContactService _emergencyServices;
        private readonly ColleagueService _colleagueService;
        private readonly ContactService _contactService;

        public UserDetailsController(GeneralDetailService generalservices,ContactService contactService,FamilyService familyService,ColleagueService colleagueService,HobbyMembershipService hobbyService, RequiredService requiredService, EmergencyContactService emergencyContactService)
        {
            _generalservices = generalservices;
            _familyService = familyService;
            _colleagueService = colleagueService;
            _requiredService = requiredService;
            _emergencyServices = emergencyContactService;
            _hobbyService = hobbyService;
            _contactService = contactService;
        }

        //post method

        [HttpPost("add-general-details/{Id}")]
        public IActionResult AddGeneral(int Id, [FromForm] GeneralVM general)
        {
            _generalservices.AddGeneral(Id, general);
            return Ok();
        }

        [HttpPost("add-PresentContact-details/{Id}")]
        public IActionResult AddPresentContact(int Id, [FromBody] ContactVM contact)
        {
            _contactService.AddPresentContact(Id, contact);
            return Ok();
        }

        [HttpPost("add-PermanentContact-details/{Id}")]
        public IActionResult AddPermanentContact(int Id, [FromBody] ContactVM contact)
        {
            _contactService.AddPermanentContact(Id, contact);
            return Ok();
        }

        [HttpPost("add-family/{empId}")]
        public async Task<IActionResult> AddFamily(int empId, [FromBody] List<FamilyVM> families)
        {
            try
            {
                _familyService.AddFamily(empId, families);
                return Ok("Sucess");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-hobby-details/{Id}")]
        public IActionResult AddHobby(int Id, [FromForm] HobbyVM hobby)
        {
            _hobbyService.AddHobby(Id, hobby);
            return Ok();
        }


        [HttpPost("add-emergency/{empId}")]
        public async Task<IActionResult> AddEmergencyContact(int empId, [FromBody] List<EmergencyContactVM> emergencies)
        {
            var data =_emergencyServices.AddEmergencyContact(empId, emergencies);
            return Ok(data + "Sucess");
        }

        [HttpPost("add-colleague/{empId}")]
        public async Task<IActionResult> AddColleague(int empId, [FromBody] List<ColleagueVM> colleagues)
        {
            _colleagueService.AddColleague(empId, colleagues);
            return Ok();
        }


        [HttpPost("add-required-details/{Id}")]
        public IActionResult AddRequired(int Id, [FromForm] RequiredVM required)
        {
            _requiredService.AddRequired(Id, required);
            return Ok();
        }


        //******************************************************************************************get method

        [HttpGet("get-general-details/{Id}")]
        public IActionResult GetGeneral(int Id)
        {
            var generaldetails = _generalservices.GetGeneral(Id);
            return Ok(generaldetails);
        }

        [HttpGet("get-contact-details/{Id}")]
        public IActionResult GetContact(int Id)
        {
            var contact = _contactService.GetContact(Id);
            return Ok(contact);
        }


        [HttpGet("get-family/{empId}")]
        public IActionResult GetFamily(int empId)
        {
            var family = _familyService.GetFamily(empId);
            return Ok(family);
        }

        [HttpGet("get-hobby-details/{Id}")]
        public IActionResult GetHobby(int Id)
        {
            var hobby = _hobbyService.GetHobby(Id);
            return Ok(hobby);
        }

        [HttpGet("get-colleague/{empId}")]
        public IActionResult GetColleague(int empId)
        {
            var colleague = _colleagueService.GetColleague(empId);
            return Ok(colleague);
        }

        [HttpGet("get-emergency/{empId}")]
        public IActionResult GetEmergencyContact(int empId)
        {
            var education = _emergencyServices.GetEmergencyContact(empId);
            return Ok(education);
        }

    }

}

