using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class PrinterBackgroundTask : BackgroundService
{
    private readonly ILogger<PrinterBackgroundTask> _logger;

    public PrinterBackgroundTask(ILogger<PrinterBackgroundTask> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Hello World");
            await Task.Delay(1000, stoppingToken);
        }
    }
}