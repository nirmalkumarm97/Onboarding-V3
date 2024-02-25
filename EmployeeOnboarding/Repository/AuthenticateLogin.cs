using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.Services;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using System.Text.Encodings.Web;

namespace EmployeeOnboarding.Repository
{

    public class AuthenticateLogin  : ILogin
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public AuthenticateLogin(ApplicationDbContext context , IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;

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
                       Role = succeeded.Role  == "U" ?  "User" : "Admin"
                   }).FirstOrDefault();

                if (_succeeded == null)
                    return null;
                else
                    return _succeeded;
            }
            catch(Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public async Task<IEnumerable<Login>> getemp()
        {
            return _context.Login.ToList();
        }

        public async Task <bool> LoginInvite(logininviteVM logindet)
        {
            try
            {
               var check = _context.Login.Where(x => x.EmailId == logindet.Emailid).FirstOrDefault();
                if (check == null)
                {
                    int Verifyotp = otpgeneration();

                    var _logindet = new Login()
                    {
                        Name = logindet.Name,
                        EmailId = logindet.Emailid,
                        Invited_Status = "Invited",
                        Date_Created = DateTime.UtcNow,
                        Date_Modified = DateTime.UtcNow,
                        Created_by = "Admin",
                        Modified_by = "Admin",
                        Status = "A",
                        Role = "U",
                        OTP = Verifyotp
                    };

                    _context.Login.Add(_logindet);
                    _context.SaveChanges();

                    var callbackUrl = "http://localhost:7136/swagger/index.html";
                    //var callbackUrl = "http://localhost:7136/api/logindetails/confirm-login";
                    ///
                    //await
                   await _emailSender.SendEmailAsync(logindet.Emailid, "Confirm your email",
                               $"Please confirm your account by entering the OTP by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'> clicking here</a>. Your OTP is " + Verifyotp);

                    return true;
                }
                else return false;
            }
            catch(Exception e)
            {
                throw new Exception(e.InnerException.Message);
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
            catch(Exception e)
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
                    await _emailSender.SendEmailAsync(emailId, "Reset Password", $"Please reset your password by entering the OTP. Your OTP is {getOTP}");
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
