using IVR_Integration_Service.Application.Interfaces;
using IVR_Integration_Service.Application.Services;
using IVR_Integration_Service.Domain.Interfaces;
using IVR_Integration_Service.Infrastructure.Persistence;
using IVR_Integration_Service.Infrastructure.Proxy;
using IVR_Integration_Service.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IVR_Integration_Service.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IVRDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IIVRRequestRepository, IVRRequestRepository>();
            services.AddScoped<IIVRItemRepository,    IVRItemRepository>();
            services.AddScoped<IIVRLogRepository,     IVRLogRepository>();

            services.AddScoped<IIVRService, IVRService>();

            services.AddTransient<IVRProviderAuthHandler>();

            services.AddHttpClient<IIVRProviderProxy, IVRProviderProxy>(client =>
            {
                var baseUrl = configuration["IVRProvider:BaseUrl"];
                if (!string.IsNullOrEmpty(baseUrl))
                    client.BaseAddress = new Uri(baseUrl);
            })
            .AddHttpMessageHandler<IVRProviderAuthHandler>();

            return services;
        }
    }
}
