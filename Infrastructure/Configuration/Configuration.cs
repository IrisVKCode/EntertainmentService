using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.IRepositories;
using Infrastructure.Repositories;
using Application.IHttpClients;
using Infrastructure.HttpClients;
using Application.Services;
using Polly;
using Polly.Extensions.Http;
using Infrastructure.BackgroundServices;

namespace Infrastructure.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }

        public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, HttpClientConfig config)
        {
            services.AddHttpClient<ITvShowClient, TvMazeClient>(client =>
            {
                client.BaseAddress = new Uri(config.TvMazeClientUrl);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreaker());

            return services;
        }

        public static IServiceCollection ConfigureInfraServices(this IServiceCollection services)
        {
            services.AddTransient<ITvShowRepository, TvShowRepository>();
            services.AddTransient<ITvShowService, TvShowService>();

            return services;
        }

        public static IServiceCollection ConfigureBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<TvShowBackgroundService>();

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreaker()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(6, TimeSpan.FromSeconds(30));
        }
    }
}
