using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundServices
{
    public class TvShowBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TvShowBackgroundService> _logger;
        private const int TaskIntervalHours = 24;

        public TvShowBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<TvShowBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"TvShowBackgroundService starts");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var tvShowService = scope.ServiceProvider.GetRequiredService<ITvShowService>();
                    _logger.LogDebug($"TvShowBackgroundService is executing..");
                    await tvShowService.RetrieveNewestShows();
                    await Task.Delay(TimeSpan.FromHours(TaskIntervalHours), stoppingToken);
                }
            }
        }
    }
}
