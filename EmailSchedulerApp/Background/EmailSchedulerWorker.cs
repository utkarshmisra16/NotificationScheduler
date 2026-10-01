namespace EmailSchedulerApp.Background
{
    public class EmailSchedulerWorker(ILogger<EmailSchedulerWorker> logger) : BackgroundService
    {
        private readonly ILogger<EmailSchedulerWorker> _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Email Scheduler Worker started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Scheduler checking for pending schedules...");
                    // Scheduler logic will be added here
                    await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    // Application is shutting down
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred in Email Scheduler Worker.");
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
            }
            _logger.LogInformation("Email Scheduler Worker stopped.");
        }
    }
}