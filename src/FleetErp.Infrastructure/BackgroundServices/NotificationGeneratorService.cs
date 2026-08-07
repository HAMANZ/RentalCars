using FleetErp.Application.Notifications.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FleetErp.Infrastructure.BackgroundServices;

/// <summary>
/// Background service that periodically generates notifications for expiring items.
/// Runs daily by default.
/// </summary>
public class NotificationGeneratorService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<NotificationGeneratorService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromHours(24); // Run once a day

    public NotificationGeneratorService(
        IServiceProvider serviceProvider,
        ILogger<NotificationGeneratorService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Notification Generator Service started");

        // Run immediately on startup
        await GenerateNotificationsAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(_interval, stoppingToken);
                await GenerateNotificationsAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Service is stopping
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating notifications");
                // Wait a shorter time before retrying on error
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }

        _logger.LogInformation("Notification Generator Service stopped");
    }

    private async Task GenerateNotificationsAsync(CancellationToken ct)
    {
        _logger.LogInformation("Generating expiration notifications...");

        using var scope = _serviceProvider.CreateScope();
        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

        await notificationService.GenerateExpirationNotificationsAsync(ct);

        _logger.LogInformation("Expiration notification generation completed");
    }
}
