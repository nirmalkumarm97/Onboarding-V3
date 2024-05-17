// StatusUpdateService.cs
using EmployeeOnboarding.Data;
using EmployeeOnboarding.Data.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace EmployeeOnboarding.BackgroundTask
{
    public class StatusUpdateService : BackgroundService
    {
        private readonly IServiceProvider _services;
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
                List<Login> entitiesToUpdate = dbContext.Login
                    .Where(x => x.Invited_Status.ToLower() == "invited" && x.Date_Created <= DateTime.UtcNow.AddDays(-7))
                    .ToList();

                if (entitiesToUpdate.Any())
                {
                    foreach (var entity in entitiesToUpdate)
                    {
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
    }
}


