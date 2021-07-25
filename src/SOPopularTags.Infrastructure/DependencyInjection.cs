using Microsoft.Extensions.DependencyInjection;
using SOPopularTags.Domain.Interfaces;
using SOPopularTags.Infrastructure.Repositories;

namespace SOPopularTags.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISOTagRequestRepository, SOTagRequestRepository>();
            return services;
        }
    }
}
