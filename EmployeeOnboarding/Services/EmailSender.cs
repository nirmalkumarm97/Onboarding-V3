using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace EmployeeOnboarding.Services
{
    public class EmailSender : IEmailSender
    {
        private string smtpServer;
        private int smtpPort;
        private string fromEmailAddress;

        public EmailSender(string smtpServer, int smtpPort, string fromEmailAddress)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.fromEmailAddress = fromEmailAddress;
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
            client.Credentials = new NetworkCredential("podupadu@outlook.com", "cholancheran12");
            client.EnableSsl = true;
            client.Send(message);
            return Task.CompletedTask;
        }
    }
}
