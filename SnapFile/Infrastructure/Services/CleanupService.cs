using Microsoft.EntityFrameworkCore;
using SnapFile.Infrastructure.Data;

namespace SnapFile.Infrastructure.Services
{
    public class CleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CleanupService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var threshold = DateTime.UtcNow.AddHours(-24);

                var usersToDelete = await db.Users
                    .Where(x => !x.IsEmailConfirmed && x.CreatedAt < threshold)
                    .ToListAsync();

                db.Users.RemoveRange(usersToDelete);

                var expiredCodes = await db.EmailCodes
                    .Where(x => x.ExpireAt < DateTime.UtcNow)
                    .ToListAsync();

                db.EmailCodes.RemoveRange(expiredCodes);

                await db.SaveChangesAsync();

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
