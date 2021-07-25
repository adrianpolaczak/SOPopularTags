using Microsoft.Extensions.DependencyInjection;
using SOPopularTags.Application.Interfaces;
using SOPopularTags.Application.Mapping;
using SOPopularTags.Application.Services;

namespace SOPopularTags.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
                cfg.AddProfile<SOTagRequestProfile>());
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IPopularityCalculatorService, PopularityCalculatorService>();
            return services;
        }
    }
}
