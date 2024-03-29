﻿using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.Services;
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

namespace EmployeeOnboarding.Repository
{

    public class AuthenticateLogin : ILogin
    {

        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;


        public AuthenticateLogin(ApplicationDbContext context, IEmailSender emailSender, IConfiguration configuration)
        {
            _context = context;
            _emailSender = emailSender;
            _configuration = configuration;
        }
        public async Task<employloginVM> AuthenticateEmp(string email, string password)
        {
            try
            {
                var _succeeded = _context.Login.Where(authUser => authUser.EmailId == email && authUser.Password == password)
                   .Select(succeeded => new employloginVM()
                   {
                       EmpId = succeeded.Id,
                       Name = succeeded.Name,
                       Email = succeeded.EmailId,
                       Role = succeeded.Role == "U" ? "User" : "Admin"
                   }).FirstOrDefault();

                if (_succeeded == null)
                    return null;
                else
                    return _succeeded;
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
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

                        if (check != null)
                        {
                            // int Verifyotp = otpgeneration();

                            check.Name = i.Name;
                            check.EmailId = i.Emailid;
                            check.Password = tempPass;
                            check.Date_Modified = DateTime.UtcNow;
                            _context.Login.Update(check);
                            _context.SaveChanges();
                        }
                        else
                        {
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
                    throw new NullReferenceException("Request is null");
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
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
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background-color: #f9f9f9;
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
        <h1>Hi {name},</h1>
        <p>Please login into your profile by <a href='{HtmlEncoder.Default.Encode(url)}'>clicking here</a>.</p>
        <p>Your email is {email} and password is {tempPass}.</p>
        <p>Regards,<br />HR Team</p>
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
                    await _emailSender.SendEmailAsync(emailId, "Reset Password", $"Please reset your password by entering the OTP. Your OTP is {getOTP}.");
                    check.OTP = getOTP;
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<string> UpdatePassword(string emailId, loginconfirmVM loginconfirmVM)
        {
            try
            {
                var check = _context.Login.FirstOrDefault(e => e.EmailId == emailId);
                if (check != null && loginconfirmVM.Password == loginconfirmVM.Conf_Password)
                {
                    check.Password = loginconfirmVM.Conf_Password;
                }
                _context.Login.Update(check);
                _context.SaveChanges();
                return "Succeeded";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<bool> VerifyOTP(string emailId, int OTP)
        {
            var CheckOtp = await _context.Login.Where(e => e.EmailId == emailId && e.OTP == OTP).FirstOrDefaultAsync();
            if (CheckOtp != null)
            {
                return true;
            }
            else return false;
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
