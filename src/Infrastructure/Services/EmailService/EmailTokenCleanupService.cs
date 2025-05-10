using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services.EmailService;

public class EmailTokenCleanupService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly TimeSpan _checkInterval = TimeSpan.FromDays(1);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await RemoveUnusedEmailTokensAsync();

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task RemoveUnusedEmailTokensAsync()
    {
        using var scope = serviceScopeFactory.CreateScope();

        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await unitOfWork.EmailTokenRepository.RemoveExpiredTokensAsync();

        await unitOfWork.SaveChangesAsync();
    }
}
