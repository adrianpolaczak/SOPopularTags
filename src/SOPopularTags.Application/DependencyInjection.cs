using Microsoft.Extensions.DependencyInjection;
using SOPopularTags.Application.Interfaces;
using SOPopularTags.Application.Services;

namespace SOPopularTags.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();
            return services;
        }
    }
}
