using DocumentFormat.OpenXml.Spreadsheet;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Handler;
using EmployeeOnboarding.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeOnboarding.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class JwtTokenController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration configuration;

        //private readonly JwtSettings _jwtSettings;
        public JwtTokenController(ApplicationDbContext applicationDbContext /*, IOptions<JwtSettings> options*/ , IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            this.configuration = configuration;
            //_jwtSettings = options.Value;
            //_jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("GenerateToken")]
        public async Task<IActionResult> GenerateToken([FromBody]JWTUser user)
        {
            if (user != null)
            {
                if (user.UserName != null && user.Password != null)
                {
                    var checkdta = _applicationDbContext.Login.Where(x => x.EmailId == user.UserName && x.Password == user.Password);
                    if (checkdta.Any())
                    {
                        var jwtkey = configuration.GetSection("JwtSettings:SecretKey").Value!;
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey));
                        var tokenDiscriptor = new SecurityTokenDescriptor
                        {
                            Issuer = configuration.GetSection("JwtSettings:Issuer").Value!,
                            Audience = configuration.GetSection("JwtSettings:Audience").Value!,
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                            new Claim(ClaimTypes.Name, ClaimTypes.Email)
                                // new Claim(ClaimTypes.Role,role)
                            }),
                            Expires = DateTime.UtcNow.AddHours(1),
                            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                        };
                        var token = new JwtSecurityTokenHandler().CreateToken(tokenDiscriptor);

                        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(jwt);
                    }
                    else return Unauthorized();
                }
                else
                {
                    return BadRequest("--> InCorrect Credentials --");
                }
            }
            else
            {
                return BadRequest("--> InCorrect Credentials --");
            }
        }
    }
}

