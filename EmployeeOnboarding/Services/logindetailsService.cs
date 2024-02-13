using EmployeeOnboarding.Data;
using EmployeeOnboarding.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using OnboardingWebsite.Models;
using System.Data;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.Routing;
using EmployeeOnboarding.Data.Enum;

namespace EmployeeOnboarding.Services
{
    public class logindetailsService
    {
        
        private ApplicationDbContext _context;
        private IEmailSender emailSender;

        public logindetailsService(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            this.emailSender = emailSender;
        }

        public int LoginInvite(logininviteVM logindet)
        {
            var _logindet = new Login()
            {
                Name = logindet.Name,
                EmailId = logindet.Emailid,
                Invited_Status="Invited",
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
            emailSender.SendEmailAsync(logindet.Emailid, "Confirm your email",
                       $"Please confirm your account by entering the OTP by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'> clicking here</a>. Your OTP is " + Verifyotp);
            return Verifyotp;

        }

        public void LoginConfirm(string Emailid,loginconfirmVM logindet)
        {
            var confirm = _context.Login.FirstOrDefault(e => e.EmailId == Emailid);
            if (logindet.Password == logindet.Conf_Password)
            {
                confirm.Password = logindet.Password;
                confirm.Invited_Status = "Confirmed";
                confirm.Date_Modified = DateTime.UtcNow;
                confirm.Modified_by = "User";
                confirm.Status = "A";

                _context.Login.Update(confirm);
                _context.SaveChanges();
            }
            else
                confirm.Password = null;

        }

        public async Task<Login> LoginCmp(string Emailid,loginconfirmVM logindet)
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

        public int otpgeneration()
        {
            int min = 1000;
            int max = 9999;
            Random rn = new Random();
            int otp = rn.Next(min,max);
            return otp;
        }
    }
}
