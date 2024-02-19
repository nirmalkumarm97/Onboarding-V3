using EmployeeOnboarding.Contracts;
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Models;
using EmployeeOnboarding.Services;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
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
                var _succeeded = _context.Login.Where(authUser => authUser.EmailId == email && authUser.Password == password).
                   Select(succeeded => new employloginVM()
                   {
                       Id = succeeded.Id,
                       Name = succeeded.Name,
                       Email = succeeded.EmailId,
                       Passwaord = succeeded.Password
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

        public int LoginInvite(logininviteVM logindet)
        {
            try
            {
               var check = _context.Login.Where(x => x.EmailId == logindet.Emailid).FirstOrDefault();
                if (check == null)
                {
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
                    };

                    _context.Login.Add(_logindet);
                    _context.SaveChanges();

                    var callbackUrl = "http://localhost:7136/swagger/index.html";
                    //var callbackUrl = "http://localhost:7136/api/logindetails/confirm-login";
                    ///
                    int Verifyotp = otpgeneration();
                    //await
                    _emailSender.SendEmailAsync(logindet.Emailid, "Confirm your email",
                               $"Please confirm your account by entering the OTP by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'> clicking here</a>. Your OTP is " + Verifyotp);
                    return Verifyotp;
                }
                else return 0;
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
        public async Task<int> ForgotPassword(string emailId)
        {
            try
            {
                var check = _context.Login.FirstOrDefault(e => e.EmailId == emailId);
                if (check != null)
                {
                    int getOTP = otpgeneration();
                    await _emailSender.SendEmailAsync(emailId, "Password Reset", $"<p>OTP : <b>\"+{getOTP}+\"</b></p>\\r\\n\\r\\n\\r\\n\\r\\n<p>Enter this OTP to Reset Password.</P>\\r\\n\\r\\n<p>OTP will Expire in 30 secs</p>\\r\\n\\r\\n\\r\\n\\r\\n<p>Note :</p>\\r\\n\\r\\n<p>1. This is an Auto Generated Mail.</p>\\r\\n\\r\\n<p>2. Don't need to Reply This mail .</p>\\r\\n\\r\\n\\r\\n\\r\\n\\r\\n<p>Regards.</p>\\r\\n\\r\\n<p>ERP</p>\"");
                    return getOTP;
                }
                else return 0;
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
