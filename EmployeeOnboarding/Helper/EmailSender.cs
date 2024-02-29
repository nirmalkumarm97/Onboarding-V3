using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace EmployeeOnboarding.Helper
{
    public class EmailSender : IEmailSender
    {
        private string smtpServer;
        private int smtpPort;
        private string fromEmailAddress;
        private string Password;

        public EmailSender(string smtpServer, int smtpPort, string fromEmailAddress, string pass)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.fromEmailAddress = fromEmailAddress;
            this.Password = pass;   
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(fromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(email));

            using var client = new SmtpClient(smtpServer, smtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(fromEmailAddress, Password);
            client.EnableSsl = true;
            client.Send(message);
            return Task.CompletedTask;
        }
    }
}
