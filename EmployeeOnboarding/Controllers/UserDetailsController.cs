using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Request;
using EmployeeOnboarding.Response;
using EmployeeOnboarding.Services;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnboardingWebsite.Models;
using System.ComponentModel;
using System.Reflection;
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
        public async Task<IActionResult> AddPersonalInfo([FromBody] PersonalInfoRequest personalInfoRequest)
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
        [HttpGet("Gender")]
        public IActionResult Gender()
        {
            var enumValues = Enum.GetValues(typeof(Gender));
            var enumDictionary = new Dictionary<string, int>();

            foreach (var value in enumValues)
            {
                var name = Enum.GetName(typeof(Gender), value);
                var id = (int)value;
                enumDictionary.Add(name, id);
            }

            return Ok(enumDictionary);

        }
        [HttpGet("MartialStatus")]
        public IActionResult MartialStatus()
        {
            var enumValues = Enum.GetValues(typeof(MartialStatus));
            var enumDictionary = new Dictionary<string, int>();

            foreach (var value in enumValues)
            {
                var name = Enum.GetName(typeof(MartialStatus), value);
                var id = (int)value;
                enumDictionary.Add(name, id);
            }

            return Ok(enumDictionary);
        }
        [HttpGet("BloodGroup")]
        public IActionResult BloodGroup()
        {

            var enumValues = Enum.GetValues(typeof(BloodGroup));
            var enumDictionary = new Dictionary<string, int>();

            foreach (var value in enumValues)
            {
                var enumType = value.GetType();
                var enumName = Enum.GetName(enumType, value);
                var memberInfo = enumType.GetMember(enumName)[0];
                var customAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();

                if (customAttribute != null)
                {
                    var customDescription = customAttribute.Description;
                    var id = (int)value;
                    enumDictionary.Add(customDescription, id);
                }
            }

            return Ok(enumDictionary);
        }

        [HttpGet("Status")]
        public IActionResult Status()
        {
            var enumValues = Enum.GetValues(typeof(Status));
            var enumDictionary = new Dictionary<string, int>();

            foreach (var value in enumValues)
            {
                var name = Enum.GetName(typeof(Status), value);
                var id = (int)value;
                enumDictionary.Add(name, id);
            }

            return Ok(enumDictionary);
        }

        [HttpGet("CovidVaccine")]
        public IActionResult CovidVaccine()
        {
            var enumValues = Enum.GetValues(typeof(CovidVaccine));
            var enumDictionary = new Dictionary<string, int>();

            foreach (var value in enumValues)
            {
                var enumType = value.GetType();
                var enumName = Enum.GetName(enumType, value);
                var memberInfo = enumType.GetMember(enumName)[0];
                var customAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();

                if (customAttribute != null)
                {
                    var customDescription = customAttribute.Description;
                    var id = (int)value;
                    enumDictionary.Add(customDescription, id);
                }
            }

            return Ok(enumDictionary);

        }


        [HttpGet("VaccinationStatus")]
        public IActionResult VaccinationStatus()
        {
            var enumValues = Enum.GetValues(typeof(VaccinationStatus));
            var enumDictionary = new Dictionary<string, int>();

            foreach (var value in enumValues)
            {
                var enumType = value.GetType();
                var enumName = Enum.GetName(enumType, value);
                var memberInfo = enumType.GetMember(enumName)[0];
                var customAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();

                if (customAttribute != null)
                {
                    var customDescription = customAttribute.Description;
                    var id = (int)value;
                    enumDictionary.Add(customDescription, id);
                }
            }

            return Ok(enumDictionary);
        }
    }
}


        

       