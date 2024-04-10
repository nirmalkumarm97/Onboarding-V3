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
using Microsoft.IdentityModel.Tokens;
using OnboardingWebsite.Models;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
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
        public async Task<IActionResult> AddPersonalInfo(bool directAdd, [FromBody] PersonalInfoRequest personalInfoRequest)
        {
            try
            {
                var response = await _userDetailsRepository.AddPersonalInfo(directAdd, personalInfoRequest);
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

        [HttpGet("GetPersonalInfo/{Id}")]
        public async Task<IActionResult> GetPersonalInfo(int Id)
        {
            try
            {
                var response = await _userDetailsRepository.GetPersonalInfo(Id);
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
        [HttpGet("GetStatusByLoginId/{loginId}")]
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
        public async Task<IActionResult> GetSates(int? id)
        {
            if (id == null)
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
            else if (id.HasValue)
            {
                var response = await _context.State.Where(x => x.Country_Id == id).ToListAsync();
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("GetCities")]
        public async Task<IActionResult> GetCities(int? id)
        {
            if (id == null)
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
            else if (id.HasValue)
            {
                var response = await _context.City.Where(x => x.State_Id == id).ToListAsync();
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("GetColleges")]
        public async Task<IActionResult> GetColleges(string? search)
        {
            try
            {
                List<string> colleges = _context.EmployeeEducationDetails
    .Where(x => (x.Qualification == 4 || x.Qualification == 5 || x.Qualification == 6)
        && (x.Institution_name.ToLower().StartsWith(search.ToLower())))
    .Select(x => x.Institution_name)
    .ToList();
                return Ok(colleges);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "An error occurred while fetching colleges."); // Internal Server Error
            }
        }


        [HttpGet("GetQualifications")]
        public async Task<IActionResult> GetQualifications()
        {
            try
            {
                var qualifications = Enum.GetValues(typeof(Qualification))
                                         .Cast<Qualification>()
                                         .Select(q => new
                                         {
                                             Id = (int)q,
                                             Name = GetEnumMemberValue(q)
                                         })
                                         .ToList();

                return Ok(qualifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching qualifications.");
            }
        }

        private string GetEnumMemberValue(Qualification value)
        {
            Type type = typeof(Qualification);
            MemberInfo[] memberInfo = type.GetMember(value.ToString());
            if (memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((EnumMemberAttribute)attrs[0]).Value;
                }
            }
            return value.ToString();
        }

        [HttpGet("GetRelationships")]
        public IActionResult GetRelationships()
        {
            try
            {
                var relationships = Enum.GetNames(typeof(Relationship)).ToList();
                return Ok(relationships);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching relationships.");
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




