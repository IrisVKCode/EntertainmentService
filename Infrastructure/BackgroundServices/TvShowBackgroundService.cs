using Application.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundServices
{
    public class TvShowBackgroundService : BackgroundService
    {
        private readonly ITvShowService _tvShowService;
        private readonly ILogger<TvShowBackgroundService> _logger;

        public TvShowBackgroundService(
            ITvShowService tvShowService,
            ILogger<TvShowBackgroundService> logger)
        {
            _tvShowService = tvShowService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"TvShowBackgroundService starts");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug($"TvShowBackgroundService is executing..");
                await _tvShowService.AddNewShowsFromApiAsync();
            }
        }
    }
}
