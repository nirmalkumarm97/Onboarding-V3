// StatusUpdateService.cs
using EmployeeOnboarding.Data;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace EmployeeOnboarding.BackgroundTask
{
    public class StatusUpdateService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private const int IntervalTime = 8 * 60 * 60; // 8 hours once trigger

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
                // Find entities where status is "Pending" and created more than 15 days ago
                List<Login> entitiesToUpdate = dbContext.Login
                    .Where(x => x.Invited_Status.ToLower() == "invited" && x.Date_Created <= DateTime.UtcNow.AddDays(-15))
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
            }, null, TimeSpan.Zero, TimeSpan.FromHours(IntervalTime));

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}


