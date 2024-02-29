using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Enum;
using EmployeeOnboarding.Request;
using EmployeeOnboarding.Response;
using EmployeeOnboarding.Services;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _context;

        public UserDetailsController(IUserDetailsRepository userDetailsRepository, ApplicationDbContext context)
        {
            _userDetailsRepository = userDetailsRepository;
            _context = context;
        }


        [HttpPost("AddPersonalInfo")]
        public async Task<IActionResult> AddPersonalInfo(bool directAdd ,[FromBody] PersonalInfoRequest personalInfoRequest)
        {
            var response = _userDetailsRepository.AddPersonalInfo(directAdd ,personalInfoRequest);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetPersonalInfo/{genId}")]
        public async Task<IActionResult> GetPersonalInfo(int genId)
        {
            var response = _userDetailsRepository.GetPersonalInfo(genId);
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
            {
                var enumValues = Enum.GetValues(typeof(Gender));
                var genderList = new List<object>();

                foreach (var value in enumValues)
                {
                    var name = Enum.GetName(typeof(Gender), value);
                    var id = (int)value;
                    var genderObj = new { Id = id, Name = name };
                    genderList.Add(genderObj);
                }

                return Ok(genderList);
            }

        }
        [HttpGet("MartialStatus")]
        public IActionResult MartialStatus()
        {
            var enumValues = Enum.GetValues(typeof(MartialStatus));
            var enumList = new List<object>();

            foreach (var value in enumValues)
            {
                var name = Enum.GetName(typeof(MartialStatus), value);
                var id = (int)value;
                var enumobj = new { Id = id, Name = name };
                enumList.Add(enumobj);
            }

            return Ok(enumList);
        }
        [HttpGet("BloodGroup")]
        public IActionResult BloodGroup()
        {
            var enumValues = Enum.GetValues(typeof(BloodGroup));
            var bloodGroupList = new List<object>();

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
                    var bloodGroupObj = new { Id = id, Name = customDescription };
                    bloodGroupList.Add(bloodGroupObj);
                }
            }

            return Ok(bloodGroupList);
        }

        [HttpGet("Status")]
        public IActionResult Status()
        {
            var enumValues = Enum.GetValues(typeof(Status));
            var enumList = new List<object>();

            foreach (var value in enumValues)
            {
                var name = Enum.GetName(typeof(Status), value);
                var id = (int)value;
                var enumobj = new { Id = id, Name = name };

                enumList.Add(enumobj);
            }

            return Ok(enumList);
        }

        [HttpGet("CovidVaccine")]
        public IActionResult CovidVaccine()
        {
            var enumValues = Enum.GetValues(typeof(CovidVaccine));
            var enumList = new List<object>();

            foreach (var value in enumValues)
            {
                var enumType = typeof(CovidVaccine); // Use the enum type directly
                var enumName = Enum.GetName(enumType, value);
                var memberInfo = enumType.GetMember(enumName)[0];
                var customAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();

                if (customAttribute != null)
                {
                    var customDescription = customAttribute.Description;
                    var id = (int)value;
                    var enumObject = new { Id = id, Name = customDescription };
                    enumList.Add(enumObject);
                }
            }

            return Ok(enumList);

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
        [HttpGet("GetStatusByLoginId")]
        public async Task<IActionResult> GetStatusByLoginId(int loginId)
        {
            var response = await _userDetailsRepository.GetStatusByLoginId(loginId);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }


        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            var response = await _context.Country.ToListAsync();
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpGet("GetStates")]
        public async Task<IActionResult> GetSates()
        {
            var response = await _context.State.ToListAsync();
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetCities")]
        public async Task<IActionResult> GetCities()
        {
            var response = await _context.City.ToListAsync();
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        //[HttpGet("GetStatesByCountryId/{Id}")]
        //public async Task<IActionResult> GetStatesByCountryId(int Id)
        //{
        //    var response = await _context.State.Where(x => x.Country_Id == Id).ToListAsync();
        //    if (response != null)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        return NoContent();
        //    }
        //}
        //[HttpGet("GetCitiesByStateId/{Id}")]
        //public async Task<IActionResult> GetCitiesByStateId(int Id)
        //{
        //    var response = await _context.City.Where(x => x.State_Id == Id).ToListAsync();
        //    if (response != null)
        //    {
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        return NoContent();
        //    }
        //}

    }
}


        

       