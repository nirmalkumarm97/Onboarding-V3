using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
//using Org.BouncyCastle.Crypto;
using System.Text.Encodings.Web;
using System.Linq;
using EmployeeOnboarding.Helper;
using EmployeeOnboarding.Response;
using DocumentFormat.OpenXml.Wordprocessing;
using EmployeeOnboarding.Data.Enum;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using EmployeeOnboarding.Data.Models;

namespace EmployeeOnboarding.Repository
{

    public class LoginRepository : ILogin
    {

        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;


        public LoginRepository(ApplicationDbContext context, IEmailSender emailSender, IConfiguration configuration)
        {
            _context = context;
            _emailSender = emailSender;
            _configuration = configuration;
        }
        public async Task<employloginVM> AuthenticateEmp(string email, string password)
        {
            if (email != null && password != null)
            {
                Login? login = await _context.Login.FirstOrDefaultAsync(authUser => authUser.EmailId == email);
                string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
                if (!Regex.IsMatch(email, pattern))
                {
                    throw new Exception("Enter a valid email id");
                }
                if (login == null)
                {
                    throw new Exception("User not found!");
                }
                if (login.Password != password)
                {
                    throw new Exception("Enter a valid password");
                }

                employloginVM loginResponse = new employloginVM
                {
                    EmpId = login.Id,
                    Name = login.Name,
                    Email = login.EmailId,
                    Role = login.Role == "U" ? "User" : "Admin"
                };
                return loginResponse;
            }
            else
            {
                throw new Exception("Null Exception");
            }
        }

        public async Task<IEnumerable<Login>> getemp()
        {
            return _context.Login.ToList();
        }

        public async Task<string> LoginInvite(List<logininviteVM> logindet)
        {
            try
            {
                
                if (logindet.Count > 0)
                {
                    foreach (var (i, check) in from i in logindet
                                               let check = _context.Login.Where(x => x.EmailId == i.Emailid).FirstOrDefault()
                                               select (i, check))
                    {
                        string tempPass = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8); // Change length as needed

                        switch (i.NewUserInvite, check)
                        {
                            case (false, not null):
                                // int Verifyotp = otpgeneration();
                                check.Name = i.Name;
                                check.EmailId = i.Emailid;
                                check.Password = tempPass;
                                check.Date_Modified = DateTime.UtcNow;
                                _context.Login.Update(check);
                                _context.SaveChanges();
                                break;

                            case (true, null):
                                var _logindet = new Login()
                                {
                                    Name = i.Name,
                                    EmailId = i.Emailid,
                                    Password = tempPass,
                                    Invited_Status = "Invited",
                                    Date_Created = DateTime.UtcNow,
                                    Date_Modified = DateTime.UtcNow,
                                    Created_by = "Admin",
                                    Modified_by = "Admin",
                                    Status = "A",
                                    Role = "U"
                                    // OTP = Verifyotp
                                };

                                _context.Login.Add(_logindet);
                                _context.SaveChanges();
                                break;

                            case (true, not null):
                                throw new Exception($"This emailId : {i.Emailid} already exists");

                            default:
                                // Handle any other cases
                                break;
                        }

                        //var callbackUrl = "http://localhost:7136/swagger/index.html";
                        ////var callbackUrl = "http://localhost:7136/api/logindetails/confirm-login";
                        ///
                        //await _emailSender.SendEmailAsync(i.Emailid, "Confirm your email",
                        //            $"Please confirm your account by entering the OTP by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'> clicking here</a>. Your OTP is " + Verifyotp);
                        //var callbackUrl = "https://onboarding-dev.ideassionlive.in/";
                        // var callbackUrl = "http://192.168.0.139:3000/otp-verification";
                        string url = _configuration.GetSection("ApplicationURL").Value;

                        await SendConfirmationEmail(i.Emailid, i.Name, url, tempPass);
                    }
                    return "Succeed";
                }
                else
                {
                    throw new Exception("Request is null");
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private async Task SendConfirmationEmail(string email, string name, string url, string tempPass)
        {
            string subject = "Confirm Your Email";
            string body = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            line-height: 1.6;
        }}
        .container {{
            max-width: 600px;
            margin: 0 auto;
            padding: 10px;
        }}
        h1 {{
            color: #333;
        }}
        p {{
            margin-bottom: 20px;
        }}
        a {{
            color: #007bff;
            text-decoration: none;
        }}
        a:hover {{
            text-decoration: underline;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <p>Dear {name},</p>
        <p>Please login into your profile by <a href='{HtmlEncoder.Default.Encode(url)}'>clicking here</a>.</p>
        <p>Your email is {email} and password is {tempPass}.</p>
        <p>Regards,<br />HR Department<br />Ideassion Technology Solutions LLP</p>
    </div>
</body>
</html>";

            try
            {
                await _emailSender.SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                // Handle email sending exceptions
                // You might want to log the exception
                throw new Exception("Error sending confirmation email: " + ex.Message);
            }
        }

        public async Task<Login> LoginCmp(string Emailid, loginconfirmVM logindet)
        {
            try
            {
                var confirm = _context.Login.FirstOrDefault(e => e.EmailId == Emailid);
                if (logindet.Password == logindet.Conf_Password)
                {
                    confirm.Password = logindet.Password;
                    confirm.Invited_Status = "Confirmed";
                    confirm.Date_Modified = DateTime.UtcNow;
                    confirm.Modified_by = "User";
                    confirm.Status = "A";
                    // confirm.Status = "Confirmed";

                    _context.Login.Update(confirm);
                    _context.SaveChanges();
                    return confirm;
                }
                else
                    return (null);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }
        public async Task<bool> ForgotPassword(string emailId)
        {
            try
            {
                var check = _context.Login.FirstOrDefault(e => e.EmailId == emailId);
                if (check != null)
                {
                    int getOTP = otpgeneration();
                    await _emailSender.SendEmailAsync(emailId, "Reset Password", $"<p>Dear {check.Name},</p>Please reset your password by entering the OTP. Your OTP is {getOTP}." +
                        $"<p>Regards,<br />HR Department<br />Ideassion Technology Solutions LLP</p>");
                    check.OTP = getOTP;
                    _context.SaveChanges();
                    return true;
                }
                throw new Exception("This emailId does not exists");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string> UpdatePassword(string emailId, loginconfirmVM loginconfirmVM)
        {
            try
            {
                var check = _context.Login.FirstOrDefault(e => e.EmailId.ToLower() == emailId.ToLower());
                if (check != null)
                {
                    if (loginconfirmVM.Password == loginconfirmVM.Conf_Password)
                    {
                        // If passwords match, update the user's password to the new password
                        check.Password = loginconfirmVM.Conf_Password;
                        _context.Login.Update(check);
                        _context.SaveChanges();
                        return "Password successfully updated";
                    }
                    else
                    {
                        // Passwords do not match
                        throw new Exception("Passwords do not match");
                    }
                }
                else
                {
                    // Email does not exist, return 400 (Bad Request)
                    throw new Exception("Email not found"); // Or any other suitable message
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<bool> VerifyOTP(string emailId, int OTP)
        {
            if (emailId != null && OTP != null)
            {
                var CheckOtp = await _context.Login.Where(e => e.EmailId == emailId && e.OTP == OTP).FirstOrDefaultAsync();
                if (CheckOtp != null)
                {
                    return true;
                }
                else throw new Exception("Enter a valid OTP");
            }
            else
            {
                throw new Exception("Null Exception");
            }
        }
        public int otpgeneration()
        {
            int min = 1000;
            int max = 9999;
            Random rn = new Random();
            int otp = rn.Next(min, max);
            return otp;
        }
    }
}
