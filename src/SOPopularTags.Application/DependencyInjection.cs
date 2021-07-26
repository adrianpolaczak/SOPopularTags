using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SOPopularTags.Application.Interfaces;
using SOPopularTags.Application.Jobs;
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

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();

                var jobKey = new JobKey("ConsumePopularTags");
                q.AddJob<ConsumePopularTagsJob>(options =>
                    options.WithIdentity(jobKey));

                q.AddTrigger(options => options
                    .ForJob(jobKey)
                    .WithIdentity($"{jobKey.Name}trigger")
                    .StartNow()
                    .WithSimpleSchedule(s => s
                        .WithInterval(TimeSpan.FromMinutes(2))
                        .RepeatForever())
                    );
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IPopularityCalculatorService, PopularityCalculatorService>();
            return services;
        }
    }
}
