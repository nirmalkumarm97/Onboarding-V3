// StatusUpdateService.cs
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace EmployeeOnboarding.BackgroundTask
{
    public class StatusUpdateService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly IEmailSender emailSender;
        private const int IntervalTime = 5; // 5 minutes 

        public StatusUpdateService(IServiceProvider services)
        {
            _services = services;
        }
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await UpdateStatus(); // Run the update logic when the service starts
            await base.StartAsync(cancellationToken);
        }

        private async Task UpdateStatus()
        {
            using (var scope = _services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                List<Login> logins = new List<Login>();
                // Find entities where status is "Pending" and created more than 7 days ago
                DateTime sevenDaysAgo = DateTime.UtcNow.AddDays(-7);
                List<Login> entitiesToUpdate = dbContext.Login
                    .Where(x => (x.Invited_Status.ToLower().Contains("invited") || x.Invited_Status.ToLower().Contains("incomplete")) && x.Date_Created <= sevenDaysAgo)
                    .ToList();

                if (entitiesToUpdate.Any())
                {
                    foreach (var entity in entitiesToUpdate)
                    {
                        await SendExpiredLoginEmail(entity.EmailId, entity.Name);
                        entity.Invited_Status = "Expired";
                        logins.Add(entity);
                    }
                }

                dbContext.UpdateRange(logins);
                await dbContext.SaveChangesAsync();
            }
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new Timer(async _ =>
            {
                await UpdateStatus();
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(IntervalTime));

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        private async Task SendExpiredLoginEmail(string email, string name)
        {
            string subject = "Your Login Has Expired";
            string body = $@"
<!DOCTYPE html>
<html>
<head>
</head>
<body>
    <div>
        <p>Dear {name},</p>
        <p>Your login has expired. Please contact the HR Department to renew your login credentials.</p>
        <p>Regards,<br />HR Department<br />Ideassion Technology Solutions LLP</p>
    </div>
</body>
</html>";
            try
            {
                await emailSender.SendEmailAsync(email, subject, body);
            }
            catch (Exception ex)
            {
                // Handle email sending exceptions
                // You might want to log the exception
                throw new Exception("Error sending confirmation email: " + ex.Message);
            }
        }
        // Additional code to send the email
    }

}



